using Microsoft.AspNetCore.Identity;

namespace FaceVerifyAttendanceSystem.DAL.Entities
{
    public class Role: IdentityRole<int>
    {
        public ICollection<User>? Users { get; set; }
    }
}
