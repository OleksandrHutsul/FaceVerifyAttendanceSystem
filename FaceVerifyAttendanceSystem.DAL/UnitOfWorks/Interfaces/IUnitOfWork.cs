using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;

namespace FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}