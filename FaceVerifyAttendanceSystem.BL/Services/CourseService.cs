using AutoMapper;
using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.DAL.Entities;
using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FaceVerifyAttendanceSystem.BL.Services
{
    public class CourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Lesson> _repository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CourseService> _logger;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, 
            IRepository<Lesson> repository, ILogger<CourseService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
            _logger = logger;
        }

        public async Task<LessonDTO> CreateCourseAsync(LessonDTO lessonDTO, ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var ownerCourse = $"{user.LastName} {user.FirstName} {user.MiddleName}".Trim();

            var entity = new Lesson
            {
                Id = default,
                NameCourse = lessonDTO.NameCourse,
                OwnerCourse = ownerCourse,
                UserId = user.Id,
                WordsCode = lessonDTO.WordsCode,
                DescriptionCourse = lessonDTO.DescriptionCourse,
                StartCourse = lessonDTO.StartCourse,
                EndCourse = lessonDTO.EndCourse
            };

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<LessonDTO>(entity);
        }

        public async Task<(IEnumerable<LessonDTO> Lessons, int TotalCount)> GetCoursesByUserPagedAsync(ClaimsPrincipal userPrincipal, int pageNumber, int pageSize)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var lessons = await _repository.Pagination(l => l.UserId == user.Id, l => l.NameCourse, pageNumber, pageSize);
            var totalCount = await _repository.CountAsync(l => l.UserId == user.Id);

            return (_mapper.Map<IEnumerable<LessonDTO>>(lessons), totalCount);
        }

        public async Task<LessonDTO?> UpdateAsync(int id, LessonDTO updatedEntity, int userId)
        {
            try
            {
                var lesson = await _repository.Get().FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
                if (lesson == null)
                {
                    _logger.LogWarning($"Lesson with id {id} not found or user {userId} does not have permission.");
                    return null;
                }

                lesson.NameCourse = updatedEntity.NameCourse;
                lesson.WordsCode = updatedEntity.WordsCode;
                lesson.DescriptionCourse = updatedEntity.DescriptionCourse;
                lesson.StartCourse = updatedEntity.StartCourse;
                lesson.EndCourse = updatedEntity.EndCourse;

                _repository.UpdateAsync(lesson);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<LessonDTO>(lesson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating lesson with id {id}");
                throw;
            }
        }

        public async Task<LessonDTO?> GetLessonByIdAsync(int id, int userId)
        {
            var lesson = await _repository.Get().FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            return _mapper.Map<LessonDTO>(lesson);
        }

        public async Task<IEnumerable<LessonDTO>> GetRandomCoursesAsync(int count)
        {
            var lessons = await _repository.GetRandomAsync(count);
            return _mapper.Map<IEnumerable<LessonDTO>>(lessons);
        }
    }
}