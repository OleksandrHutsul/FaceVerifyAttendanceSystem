namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public required DateTime Date { get; set; }
        public required bool Attended { get; set; }

        public required int UserId { get; set; }
        public virtual User User { get; set; }

        public required int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}