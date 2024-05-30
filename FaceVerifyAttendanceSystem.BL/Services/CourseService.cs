using AutoMapper;
using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.DAL.Entities;
using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FaceVerifyAttendanceSystem.BL.Services
{
    public class CourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Lesson> _repository;
        private readonly UserManager<User> _userManager;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IRepository<Lesson> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
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

            var ownerCourse = $"{user.LastName} {user.FirstName} {user.MiddleName}".Trim();
            var lessons = await _repository.Pagination(l => l.OwnerCourse == ownerCourse, l => l.NameCourse, pageNumber, pageSize);
            var totalCount = await _repository.CountAsync(l => l.OwnerCourse == ownerCourse);

            return (_mapper.Map<IEnumerable<LessonDTO>>(lessons), totalCount);
        }

    }
}