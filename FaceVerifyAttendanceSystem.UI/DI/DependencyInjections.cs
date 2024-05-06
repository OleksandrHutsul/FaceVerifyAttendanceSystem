using FaceVerifyAttendanceSystem.DAL.Repositories;
using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;

namespace FaceVerifyAttendanceSystem.UI.DI
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}