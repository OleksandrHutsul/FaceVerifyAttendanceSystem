using FaceVerifyAttendanceSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaceVerifyAttendanceSystem.DAL.Context
{
    public class ApiDbContext : IdentityDbContext<User, Role, int>
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
    }
}