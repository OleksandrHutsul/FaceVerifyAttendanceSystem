using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class WordsCodeDTO
    {
        [Required(ErrorMessage = "Words code is required")]
        public string WordsCode { get; set; }
    }
}