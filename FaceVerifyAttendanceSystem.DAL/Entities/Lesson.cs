namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public required string NameCourse { get; set; }
        public string? DescriptionCourse { get; set; }
        public DateTime? StartCourse { get; set; }
        public DateTime? EndCourse { get; set; }
        public string? OwnerCourse { get; set; }
        public int UserId { get; set; }
        public required string WordsCode { get; set; }
        public ICollection<UserLesson>? UserLessons { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}