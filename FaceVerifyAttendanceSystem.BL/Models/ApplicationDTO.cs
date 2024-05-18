using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class ApplicationDTO
    {
        [Required]
        [Display(Name = "Name of the department")]
        public string NameDepartment { get; set; }
    }
}
