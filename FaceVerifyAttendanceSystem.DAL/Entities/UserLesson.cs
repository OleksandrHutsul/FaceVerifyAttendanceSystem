namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class UserLesson
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}