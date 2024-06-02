using FaceVerifyAttendanceSystem.BL.Services;
using FaceVerifyAttendanceSystem.DAL.Repositories;
using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FaceVerifyAttendanceSystem.UI.DI
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<AdminService>();
            services.AddTransient<CourseService>();
            services.AddTransient<InformationService>();
            services.AddTransient<IEmailSender, EmailSenderService>();
        }
    }
}