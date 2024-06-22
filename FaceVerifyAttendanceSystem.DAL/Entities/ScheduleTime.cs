namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class ScheduleTime
    {
        public int Id { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
    }
}