using AutoMapper;
using FaceVerifyAttendanceSystem.DAL.Entities;
using FaceVerifyAttendanceSystem.BL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FaceVerifyAttendanceSystem.BL.Services
{
    public class AdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ApplicationDTO>> GetAllApplicationsAsync(ApplicationFilterParams filterParams)
        {
            var applicationRepository = _unitOfWork.GetRepository<Application>();

            var query = applicationRepository
                .Get()
                .Include(a => a.User)
                .Include(a => a.ApplicationStatus)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterParams.StatusFilter))
            {
                query = query.Where(a => a.ApplicationStatus.StatusName == filterParams.StatusFilter);
            }

            switch (filterParams.SortField)
            {
                case "NameDepartment":
                    query = filterParams.SortOrder == "asc" ? query.OrderBy(a => a.NameDepartment) : query.OrderByDescending(a => a.NameDepartment);
                    break;
                case "User.Email":
                    query = filterParams.SortOrder == "asc" ? query.OrderBy(a => a.User.Email) : query.OrderByDescending(a => a.User.Email);
                    break;
                case "User.FirstName":
                    query = filterParams.SortOrder == "asc" ? query.OrderBy(a => a.User.FirstName) : query.OrderByDescending(a => a.User.FirstName);
                    break;
                case "User.LastName":
                    query = filterParams.SortOrder == "asc" ? query.OrderBy(a => a.User.LastName) : query.OrderByDescending(a => a.User.LastName);
                    break;
                case "User.MiddleName":
                    query = filterParams.SortOrder == "asc" ? query.OrderBy(a => a.User.MiddleName) : query.OrderByDescending(a => a.User.MiddleName);
                    break;
                case "User.EducationalInstitution":
                    query = filterParams.SortOrder == "asc" ? query.OrderBy(a => a.User.EducationalInstitution) : query.OrderByDescending(a => a.User.EducationalInstitution);
                    break;
                case "ApplicationStatus.StatusName":
                    query = filterParams.SortOrder == "asc" ? query.OrderBy(a => a.ApplicationStatus.StatusName) : query.OrderByDescending(a => a.ApplicationStatus.StatusName);
                    break;
            }

            var applications = await query.ToListAsync();

            return applications.Select(application => new ApplicationDTO
            {
                Id = application.Id,
                NameDepartment = application.NameDepartment,
                User = _mapper.Map<ExternalLoginDTO>(application.User),
                ApplicationStatus = _mapper.Map<ApplicationStatusDTO>(application.ApplicationStatus)
            }).ToList();
        }

        public async Task<bool> ChangeApplicationStatusAsync(int applicationId, int newStatusId)
        {
            var applicationRepository = _unitOfWork.GetRepository<Application>();
            var application = await applicationRepository.Get().Include(a => a.User).FirstOrDefaultAsync(a => a.Id == applicationId);

            if (application == null)
            {
                Console.WriteLine("Application not found");
                return false;
            }

            if (application.ApplicationStatusId == 2 || application.ApplicationStatusId == 3)
            {
                Console.WriteLine("Application status cannot be changed");
                return false;
            }

            application.ApplicationStatusId = newStatusId;
            await applicationRepository.UpdateAsync(application);

            if (newStatusId == 2 && application.User != null)
            {
                var user = application.User;
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (currentRoles.Contains("Student"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Student");
                    await _userManager.AddToRoleAsync(user, "Teacher");
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
