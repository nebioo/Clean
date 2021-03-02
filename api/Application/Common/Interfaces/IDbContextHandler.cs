using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Common.Interfaces
{
    public interface IDbContextHandler
    {
        Task SaveChangesAsync();
    }
}
