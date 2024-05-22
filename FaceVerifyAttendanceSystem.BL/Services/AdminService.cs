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

        public async Task<List<ApplicationDTO>> GetAllApplicationsAsync()
        {
            var applicationRepository = _unitOfWork.GetRepository<Application>();

            var applications = await applicationRepository
                .Get()
                .Include(a => a.User)
                .Include(a => a.ApplicationStatus)
                .ToListAsync();

            var applicationDTOs = new List<ApplicationDTO>();

            foreach (var application in applications)
            {
                var applicationDTO = new ApplicationDTO
                {
                    Id = application.Id,
                    NameDepartment = application.NameDepartment,
                    User = _mapper.Map<ExternalLoginDTO>(application.User),
                    ApplicationStatus = _mapper.Map<ApplicationStatusDTO>(application.ApplicationStatus)
                };

                applicationDTOs.Add(applicationDTO);
            }

            return applicationDTOs;
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
