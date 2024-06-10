using AutoMapper;
using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.DAL.Entities;
using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FaceVerifyAttendanceSystem.BL.Services
{
    public class InformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly IRepository<UserLesson> _userLessonRepository;
        private readonly IRepository<Attendance> _attendanceRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<InformationService> _logger;

        public InformationService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager,
            IRepository<Lesson> lessonRepository, IRepository<UserLesson> userLessonRepository, 
            IRepository<Attendance> attendanceRepository, ILogger<InformationService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _lessonRepository = lessonRepository;
            _userLessonRepository = userLessonRepository;
            _attendanceRepository = attendanceRepository;
            _logger = logger;
        }

        public async Task<LessonDTO?> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.Get().FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<LessonDTO>(lesson);
        }

        public async Task<bool> IsUserEnrolledAsync(int courseId, int userId)
        {
            var isEnrolled = await _userLessonRepository.Get().AnyAsync(ul => ul.LessonId == courseId && ul.UserId == userId);
            if (!isEnrolled)
            {
                _logger.LogWarning($"User with id {userId} is not enrolled in course {courseId}.");
            }
            return isEnrolled;
        }

        public async Task<bool> EnrollUserInCourseAsync(int courseId, int userId, string wordsCode, bool isAuthor = false)
        {
            var lesson = await _lessonRepository.Get().FirstOrDefaultAsync(x => x.Id == courseId);

            if (lesson == null)
            {
                _logger.LogWarning($"Lesson with id {courseId} not found.");
                return false;
            }

            _logger.LogInformation($"Lesson found: Id={lesson.Id}, WordsCode={lesson.WordsCode}");

            if (lesson.WordsCode == wordsCode || isAuthor)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    _logger.LogWarning($"User with id {userId} not found.");
                    return false;
                }

                var userLesson = new UserLesson { UserId = userId, LessonId = courseId };
                await _userLessonRepository.AddAsync(userLesson);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"User {userId} enrolled in course {courseId}.");
                return true;
            }
            else
            {
                _logger.LogWarning($"Incorrect words code for course {courseId}. Expected: {lesson.WordsCode}, provided: {wordsCode}");
            }

            return false;
        }

        public async Task<CourseDetailDTO?> GetCourseDetailAsync(int courseId)
        {
            var lesson = await _lessonRepository.Get()
                .Include(l => l.UserLessons)
                .ThenInclude(ul => ul.User)
                .FirstOrDefaultAsync(l => l.Id == courseId);

            if (lesson == null)
            {
                _logger.LogWarning($"Lesson with id {courseId} not found.");
                return null;
            }

            var teacher = await _userManager.FindByIdAsync(lesson.UserId.ToString());
            if (teacher == null)
            {
                _logger.LogWarning($"Teacher with id {lesson.UserId} not found.");
                return null;
            }

            var enrolledUsers = new List<UserDTO>();

            foreach (var ul in lesson.UserLessons)
            {
                var roles = await _userManager.GetRolesAsync(ul.User);
                enrolledUsers.Add(new UserDTO
                {
                    Id = ul.UserId,
                    LastName = ul.User.LastName,
                    FirstName = ul.User.FirstName,
                    MiddleName = ul.User.MiddleName,
                    Email = ul.User.Email,
                    Roles = roles.ToList()
                });
            }

            var courseDetail = new CourseDetailDTO
            {
                Id = lesson.Id,
                NameCourse = lesson.NameCourse,
                DescriptionCourse = lesson.DescriptionCourse,
                StartCourse = lesson.StartCourse ?? DateTime.MinValue,
                EndCourse = lesson.EndCourse ?? DateTime.MinValue,
                OwnerCourse = $"{teacher.LastName} {teacher.FirstName} {teacher.MiddleName}",
                EnrolledUsers = enrolledUsers
            };

            return courseDetail;
        }

        public async Task<List<AttendanceDTO>> GetAttendanceByCourseAsync(int courseId)
        {
            var attendances = await _attendanceRepository.Get()
                .Where(a => a.LessonId == courseId)
                .ToListAsync();

            return _mapper.Map<List<AttendanceDTO>>(attendances);
        }
    }
}