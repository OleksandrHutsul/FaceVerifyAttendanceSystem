using Microsoft.AspNetCore.Identity;

namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string EducationalInstitution { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? CourseEducation { get; set; }
        public int? IdentificationNumber { get; set; }
        public string? UploadPhoto { get; set; }

        public int? RoleId { get; set; } = 1;
        public Role? Role {  get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
}