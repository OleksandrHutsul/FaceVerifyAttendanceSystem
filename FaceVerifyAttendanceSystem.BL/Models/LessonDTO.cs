using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    [ValidCourseDate]
    public class LessonDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "NameCourse is required")]
        public string NameCourse { get; set; }
        public string? OwnerCourse { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "WordsCode is required")]
        public string WordsCode { get; set; }
        public string? DescriptionCourse { get; set; }
        [Required(ErrorMessage = "StartCourse is required")]
        [DataType(DataType.Date)]
        public DateTime? StartCourse { get; set; }
        [Required(ErrorMessage = "EndCourse is required")]
        [DataType(DataType.Date)]
        public DateTime? EndCourse { get; set; }
        public ICollection<UserDTO>? User { get; set; }
    }
}