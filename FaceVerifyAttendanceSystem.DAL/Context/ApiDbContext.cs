using FaceVerifyAttendanceSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaceVerifyAttendanceSystem.DAL.Context
{
    public class ApiDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public virtual DbSet<UserLesson> UserLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
               .HasOne(a => a.User)
               .WithOne(u => u.Application)
               .HasForeignKey<Application>(a => a.UserId);

            modelBuilder.Entity<UserLesson>()
                .HasKey(ul => new { ul.UserId, ul.LessonId });

            modelBuilder.Entity<UserLesson>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLessons)
                .HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<UserLesson>()
                .HasOne(ul => ul.Lesson)
                .WithMany(l => l.UserLessons)
                .HasForeignKey(ul => ul.LessonId);
        }
    }
}