using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FaceVerifyAttendanceSystem.UI.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ScheduleController : Controller
    {
        private readonly ScheduleService _scheduleService;
        private readonly UserManager<User> _userManager;

        public ScheduleController(ScheduleService scheduleService, UserManager<User> userManager)
        {
            _scheduleService = scheduleService;
            _userManager = userManager;
        }

        private async Task<int> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return View(schedules);
        }

        public async Task<IActionResult> Create()
        {
            int userId = await GetCurrentUserId();
            ViewBag.ScheduleTimes = await _scheduleService.GetAllScheduleTimesAsync();
            ViewBag.DaysOfWeek = await _scheduleService.GetAllDaysOfWeekAsync();
            ViewBag.Lessons = await _scheduleService.GetLessonsByTeacherAsync(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleDTO dto)
        {
            int userId = await GetCurrentUserId();

            if (ModelState.IsValid)
            {
                var schedule = new Schedule
                {
                    Location = dto.Location,
                    ScheduleTimeId = dto.ScheduleTimeId,
                    ScheduleDayOfWeekId = dto.ScheduleDayOfWeekId,
                    LessonId = dto.LessonId
                };

                await _scheduleService.AddScheduleAsync(schedule);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ScheduleTimes = await _scheduleService.GetAllScheduleTimesAsync();
            ViewBag.DaysOfWeek = await _scheduleService.GetAllDaysOfWeekAsync();
            ViewBag.Lessons = await _scheduleService.GetLessonsByTeacherAsync(userId);
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            int userId = await GetCurrentUserId();

            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var dto = new ScheduleDTO
            {
                Id = schedule.Id,
                Location = schedule.Location,
                ScheduleTimeId = schedule.ScheduleTimeId,
                ScheduleDayOfWeekId = schedule.ScheduleDayOfWeekId,
                LessonId = schedule.LessonId
            };

            ViewBag.ScheduleTimes = await _scheduleService.GetAllScheduleTimesAsync();
            ViewBag.DaysOfWeek = await _scheduleService.GetAllDaysOfWeekAsync();
            ViewBag.Lessons = await _scheduleService.GetLessonsByTeacherAsync(userId);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ScheduleDTO dto)
        {
            int userId = await GetCurrentUserId();
            if (ModelState.IsValid)
            {
                var schedule = new Schedule
                {
                    Id = dto.Id,
                    Location = dto.Location,
                    ScheduleTimeId = dto.ScheduleTimeId,
                    ScheduleDayOfWeekId = dto.ScheduleDayOfWeekId,
                    LessonId = dto.LessonId
                };

                await _scheduleService.UpdateScheduleAsync(schedule);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ScheduleTimes = await _scheduleService.GetAllScheduleTimesAsync();
            ViewBag.DaysOfWeek = await _scheduleService.GetAllDaysOfWeekAsync();
            ViewBag.Lessons = await _scheduleService.GetLessonsByTeacherAsync(userId);
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _scheduleService.DeleteScheduleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}