using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FaceVerifyAttendanceSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CourseService _courseService;

        public HomeController(ILogger<HomeController> logger, CourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            const int pageSize = 6;
            const int randomCourseCount = 30;

            var courses = await _courseService.GetRandomCoursesAsync(randomCourseCount);

            int totalCourses = courses.Count();
            int totalPages = (int)Math.Ceiling(totalCourses / (double)pageSize);

            var paginatedCourses = courses.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedCourses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}