using Microsoft.AspNetCore.Identity;

namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? EducationalInstitution { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? CourseEducation { get; set; }
        public int? IdentificationNumber { get; set; }

        public int? ApplicationId { get; set; }
        public Application? Application { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<UserLesson>? UserLessons { get; set; }
    }
}