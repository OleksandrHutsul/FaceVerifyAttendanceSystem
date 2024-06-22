namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public required string Location { get; set; }

        public required int ScheduleTimeId {  get; set; }
        public virtual ScheduleTime ScheduleTime { get; set; }
        public required int ScheduleDayOfWeekId {  get; set; }
        public virtual ScheduleDayOfWeek ScheduleDayOfWeek { get; set; }
        public required int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}