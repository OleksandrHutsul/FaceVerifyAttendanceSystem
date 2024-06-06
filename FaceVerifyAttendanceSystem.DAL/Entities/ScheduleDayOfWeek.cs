namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class ScheduleDayOfWeek
    {
        public int Id { get; set; }
        public required DayOfWeek DayOfWeek { get; set; }
    }
}
