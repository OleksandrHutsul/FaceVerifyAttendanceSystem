using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class IndexDTO
    {
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Educational Institution")]
        public string EducationalInstitution { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        [Required]
        [Range(100000, 999999, ErrorMessage = "Identification Number must be a 6-digit number.")]
        [Display(Name = "Identification Number")]
        public int? IdentificationNumber { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Required]
        [RegularExpression(@"\d{3}", ErrorMessage = "Group number must be a 3-digit number.")]
        [Display(Name = "Group number")]
        public string CourseEducation { get; set; }

        public IFormFile? Photo { get; set; }

        public string? ProfilePicture { get; set; }
    }
}