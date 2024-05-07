using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class ExternalLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string EducationalInstitution { get; set; }
        
        [Required]
        public string UserName { get; set; }
    }
}
