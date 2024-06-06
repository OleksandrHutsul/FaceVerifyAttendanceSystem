using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int ScheduleTimeId { get; set; }
        [Required]
        public int ScheduleDayOfWeekId { get; set; }
        [Required]
        public int LessonId { get; set; }
    }
}