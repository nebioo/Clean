using Domain.FileAggregate;
using Infrastructure.Persistence;

namespace Infrastructure.RepositoryAggregate
{
    public class FileRepository : GenericRepository<File>, IFileRepository
    {
        public FileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
