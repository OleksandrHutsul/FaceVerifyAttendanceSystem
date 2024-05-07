using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class LoginWithRecoveryCodeDTO
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}