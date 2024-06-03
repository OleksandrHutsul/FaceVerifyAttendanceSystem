using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class CourseDetailDTO
    {
        public string NameCourse { get; set; }
        public string DescriptionCourse { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartCourse { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndCourse { get; set; }
        public string OwnerCourse { get; set; }
        public List<UserDTO> EnrolledUsers { get; set; }
    }
}
