using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FaceVerifyAttendanceSystem.UI.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
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
                var userPrincipal = HttpContext.User;
                var createdCourse = await _courseService.CreateCourseAsync(lessonDTO, userPrincipal);
                return RedirectToAction(nameof(Index));
            }

            foreach (var state in ModelState)
            {
                if (state.Value.Errors.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Property: {state.Key}, Error: {state.Value.Errors[0].ErrorMessage}");
                }
            }

            return View(lessonDTO);
        }
    }
}
