using AutoMapper;
using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.DAL.Entities;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FaceVerifyAttendanceSystem.UI.Areas.Identity.Pages.Account.Manage
{
    public class ChangeRoleModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeRoleModel(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ApplicationDTO ApplicationDTO { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(p => p.ErrorMessage));
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var applicationRepository = _unitOfWork.GetRepository<Application>();

            var existingApplication = (await applicationRepository.FindAsync(a => a.UserId == user.Id)).FirstOrDefault();

            if (existingApplication != null)
            {
                _mapper.Map(ApplicationDTO, existingApplication);
                await applicationRepository.UpdateAsync(existingApplication);
            }
            else
            {
                var newApplication = _mapper.Map<Application>(ApplicationDTO);
                newApplication.UserId = user.Id;
                await applicationRepository.AddAsync(newApplication);
            }

            await _unitOfWork.SaveChangesAsync();

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}