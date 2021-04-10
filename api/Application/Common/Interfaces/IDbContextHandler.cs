using System.Threading.Tasks;

namespace ApplicationService.Common.Interfaces
{
    public interface IDbContextHandler
    {
        Task SaveChangesAsync();
    }
}
