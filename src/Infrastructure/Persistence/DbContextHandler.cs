using System;
using System.Threading.Tasks;
using ApplicationService.Common;
using Domain;

namespace Infrastructure.Persistence;

public class DbContextHandler : IDbContextHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IAppLogger _appLogger;

    public DbContextHandler(ApplicationDbContext dbContext, IAppLogger appLogger)
    {
        _dbContext = dbContext;
        _appLogger = appLogger;
    }
    public async Task SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _appLogger.Exception(ex, null);
            _dbContext.ChangeTracker.Clear();
            throw ex;
        }
    }
}
