using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class AttendanceDTO
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public bool Attended { get; set; } = false;

        public int UserId { get; set; }
        public int LessonId { get; set; }
    }
}