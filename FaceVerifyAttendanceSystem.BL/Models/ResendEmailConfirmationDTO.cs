using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class ResendEmailConfirmationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}