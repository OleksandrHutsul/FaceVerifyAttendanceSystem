using FaceVerifyAttendanceSystem.BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FaceVerifyAttendanceSystem.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var applications = await _adminService.GetAllApplicationsAsync();
            return View(applications);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeApplicationStatus(int applicationId, int newStatusId)
        {
            var result = await _adminService.ChangeApplicationStatusAsync(applicationId, newStatusId);
            if (result)
            {
                TempData["SuccessMessage"] = "Application status changed successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to change application status.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}