using System.Threading.Tasks;
using Domain.Common;

namespace Domain
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : DomainBase;
        Task<int> CommitAsync();
        void Rollback();
    }
}

