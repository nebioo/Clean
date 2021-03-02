using ApplicationService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DbContextHandler : IDbContextHandler
    {
        private readonly ApplicationDbContext _dbContext;

        public DbContextHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}