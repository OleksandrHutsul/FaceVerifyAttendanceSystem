namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public required string NameDepartment { get; set; }
        public string? AdminComment { get; set; }

        public required int UserId { get; set; }
        public virtual User User { get; set; }

        public required int ApplicationStatusId { get; set; }
        public virtual ApplicationStatus ApplicationStatus { get; set; }
    }
}