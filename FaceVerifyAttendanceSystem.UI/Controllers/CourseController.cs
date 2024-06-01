using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaceVerifyAttendanceSystem.UI.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(CourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 6)
        {
            var userPrincipal = HttpContext.User;
            var (courses, totalCount) = await _courseService.GetCoursesByUserPagedAsync(userPrincipal, pageNumber, pageSize);

            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.PageNumber = pageNumber;

            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonDTO lessonDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userPrincipal = HttpContext.User;
                    var createdCourse = await _courseService.CreateCourseAsync(lessonDTO, userPrincipal);
                    TempData["SuccessMessage"] = "Course created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating course.");
                    TempData["ErrorMessage"] = "An error occurred while creating the course.";
                }
            }
            return View(lessonDTO);
        }

        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lessonDTO = await _courseService.GetLessonByIdAsync(id);
            if (lessonDTO == null)
            {
                TempData["ErrorMessage"] = "Course not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(lessonDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [FromForm] LessonDTO lessonDTO)
        {
            if (!ModelState.IsValid)
                return View(lessonDTO);

            try
            {
                var updatedEntity = await _courseService.UpdateAsync(id, lessonDTO);
                if (updatedEntity == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["SuccessMessage"] = "Course updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating lesson with id {id}");
                TempData["ErrorMessage"] = "An error occurred while updating the course.";
                return View(lessonDTO);
            }
        }
    }
}