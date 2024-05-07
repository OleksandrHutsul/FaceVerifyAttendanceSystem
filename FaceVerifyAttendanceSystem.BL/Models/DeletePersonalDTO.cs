using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class DeletePersonalDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}