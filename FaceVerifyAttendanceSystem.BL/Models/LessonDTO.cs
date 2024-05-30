using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class LessonDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "NameCourse is required")]
        public string NameCourse { get; set; }

        public string? OwnerCourse { get; set; }

        [Required(ErrorMessage = "WordsCode is required")]
        public string WordsCode { get; set; }

        public string? DescriptionCourse { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartCourse { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndCourse { get; set; }

        public ICollection<UserDTO>? User { get; set; }
    }
}