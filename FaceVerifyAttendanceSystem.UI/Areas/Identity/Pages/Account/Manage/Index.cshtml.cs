﻿#nullable disable

using AutoMapper;
using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.BL.Services;
using FaceVerifyAttendanceSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FaceVerifyAttendanceSystem.UI.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private const string credentialPath = "credentials.json";
        private const string folderId = "1XyKmDPzNCJVIUze8wNATXKakwOPni2n4";
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public IndexDTO IndexDTO { get; set; }

        private async Task LoadAsync(User user)
        {
            var userFromDb = await _userManager.FindByIdAsync((user.Id).ToString());

            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;

            IndexDTO = _mapper.Map<IndexDTO>(userFromDb);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(p => p.ErrorMessage));
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (IndexDTO.Photo != null)
            {
                var photoUrl = await UploadPhotoService.UploadProfilePhoto(credentialPath, folderId, IndexDTO.Photo, user.FirstName,
                    user.MiddleName, user.LastName, user.IdentificationNumber);
                IndexDTO.ProfilePicture = photoUrl;
            }

            if (User.IsInRole("Teacher"))
            {
                IndexDTO.IdentificationNumber = 100000;
                IndexDTO.CourseEducation = "000";
            }

            var userWithSameIdNumber = _userManager.Users.FirstOrDefault(u => u.IdentificationNumber == IndexDTO.IdentificationNumber && u.Id != user.Id);
            if (userWithSameIdNumber != null)
            {
                ModelState.AddModelError("IndexDTO.IdentificationNumber", "Identification number must be unique.");
                return Page();
            }

            _mapper.Map(IndexDTO, user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update profile.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePhotoAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                var fileId = user.ProfilePicture.Split("id=")[1];
                UploadPhotoService.DeleteFileFromGoogleDrive(credentialPath, fileId);
            }

            user.ProfilePicture = null;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to delete photo.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your photo has been deleted";
            return RedirectToPage();
        }
    }
}