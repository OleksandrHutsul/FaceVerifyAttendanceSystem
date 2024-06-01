using FaceVerifyAttendanceSystem.BL.Models;
using System.ComponentModel.DataAnnotations;

public class ValidCourseDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var lesson = (LessonDTO)validationContext.ObjectInstance;

        if (lesson.StartCourse.HasValue)
        {
            if (lesson.StartCourse.Value < new DateTime(2024, 2, 1))
            {
                return new ValidationResult("StartCourse must be on or after 01.02.2024.");
            }

            if (lesson.EndCourse.HasValue)
            {
                if (lesson.EndCourse.Value < lesson.StartCourse.Value.AddMonths(4))
                {
                    return new ValidationResult("EndCourse must be at least 4 months after StartCourse.");
                }

                if (lesson.EndCourse.Value < lesson.StartCourse.Value)
                {
                    return new ValidationResult("EndCourse cannot be earlier than StartCourse.");
                }
            }
        }

        return ValidationResult.Success;
    }
}