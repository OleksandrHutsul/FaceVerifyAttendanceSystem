using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class ApplicationDTO
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name of the department")]
        public string NameDepartment { get; set; }

        public int StatusId { get; set; } = 1;
    }
}
