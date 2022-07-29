using System;
using System.Threading.Tasks;

namespace Domain;

public interface IDbContextHandler
{
    Task SaveChangesAsync();
}
