using System.ComponentModel.DataAnnotations.Schema;

namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public required string NameCourse { get; set; }
        public DateTime? StartCourse { get; set; }
        public DateTime? EndCourse { get; set; }
        public required string OwnerCourse { get; set; }
        public required string WordsCode {  get; set; }

        public required int UserId { get; set; }
        public virtual User User { get; set; }
    }
}