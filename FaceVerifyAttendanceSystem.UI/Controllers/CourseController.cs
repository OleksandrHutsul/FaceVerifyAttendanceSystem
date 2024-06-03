using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.BL.Services;
using FaceVerifyAttendanceSystem.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FaceVerifyAttendanceSystem.UI.Controllers
{
    [AllowAnonymous]
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;
        private readonly InformationService _informationService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CourseController> _logger;

        public CourseController(CourseService courseService, InformationService informationService, ILogger<CourseController> logger, UserManager<User> userManager)
        {
            _courseService = courseService;
            _logger = logger;
            _userManager = userManager;
            _informationService = informationService;
        }

        #region
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 6)
        {
            var userPrincipal = HttpContext.User;
            var (courses, totalCount) = await _courseService.GetCoursesByUserPagedAsync(userPrincipal, pageNumber, pageSize);

            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.PageNumber = pageNumber;

            return View(courses);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create(LessonDTO lessonDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userPrincipal = HttpContext.User;
                    var createdCourse = await _courseService.CreateCourseAsync(lessonDTO, userPrincipal);
                    await _informationService.EnrollUserInCourseAsync(createdCourse.Id, createdCourse.UserId, lessonDTO.WordsCode, true);
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
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            var lessonDTO = await _courseService.GetLessonByIdAsync(id, user.Id);
            if (lessonDTO == null)
            {
                TempData["ErrorMessage"] = "Course not found or you do not have permission to edit this course.";
                return RedirectToAction(nameof(Index));
            }

            return View(lessonDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id, [FromForm] LessonDTO lessonDTO)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
                return View(lessonDTO);

            try
            {
                var updatedEntity = await _courseService.UpdateAsync(id, lessonDTO, user.Id);
                if (updatedEntity == null)
                {
                    TempData["ErrorMessage"] = "Course not found or you do not have permission to edit this course.";
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
        #endregion

        [HttpGet]
        [Authorize(Policy = "TeacherOrStudentPolicy")]
        public async Task<IActionResult> Information(int courseId)
        {
            if (courseId == 0)
            {
                TempData["ErrorMessage"] = "Invalid course ID.";
                return RedirectToAction("Error", "Home");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Error", "Home");
            }

            var isEnrolled = await _informationService.IsUserEnrolledAsync(courseId, user.Id);
            if (isEnrolled)
            {
                TempData["Message"] = "You are already enrolled in this course.";
                return RedirectToAction("Enrolled", new { courseId });
            }

            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "TeacherOrStudentPolicy")]
        public async Task<IActionResult> Information(int courseId, WordsCodeDTO wordsCodeDTO)
        {
            if (courseId == 0)
            {
                TempData["ErrorMessage"] = "Invalid course ID.";
                return RedirectToAction("Error", "Home");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Error", "Home");
            }

            //if (!user.Email.Contains("edu"))
            //{
            //    TempData["ErrorMessage"] = "Registration is allowed only with educational email addresses.";
            //    return RedirectToAction("Error", "Home");
            //}

            if (!ModelState.IsValid)
            {
                ViewBag.CourseId = courseId;
                return View(wordsCodeDTO);
            }

            var isEnrolled = await _informationService.IsUserEnrolledAsync(courseId, user.Id);
            if (isEnrolled)
            {
                TempData["Message"] = "You are already enrolled in this course.";
                return RedirectToAction("Enrolled", new { courseId });
            }

            var success = await _informationService.EnrollUserInCourseAsync(courseId, user.Id, wordsCodeDTO.WordsCode);
            if (success)
            {
                TempData["SuccessMessage"] = "You have successfully enrolled in the course.";
                return RedirectToAction("Enrolled", new { courseId });
            }

            TempData["ErrorMessage"] = "Incorrect word code.";
            ViewBag.CourseId = courseId;
            return View(wordsCodeDTO);
        }

        [HttpGet]
        [Authorize(Policy = "TeacherOrStudentPolicy")]
        public async Task<IActionResult> Enrolled(int courseId)
        {
            if (courseId == 0)
            {
                TempData["ErrorMessage"] = "Invalid course ID.";
                return RedirectToAction("Error", "Home");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Error", "Home");
            }

            var courseDetail = await _informationService.GetCourseDetailAsync(courseId);
            if (courseDetail == null)
            {
                TempData["ErrorMessage"] = "Course not found.";
                return RedirectToAction("Error", "Home");
            }

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.UserRoles = string.Join(", ", roles);

            ViewBag.CourseDetail = courseDetail;
            return View();
        }
    }
}