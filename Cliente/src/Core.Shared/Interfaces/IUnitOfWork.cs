using System.Threading.Tasks;

namespace Core.Shared
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();

        Task Rollback();

    }
}
