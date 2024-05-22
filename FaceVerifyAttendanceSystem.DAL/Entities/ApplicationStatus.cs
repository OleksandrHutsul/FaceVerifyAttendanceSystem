namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class ApplicationStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}