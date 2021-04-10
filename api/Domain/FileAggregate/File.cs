using Domain.Common;
using Domain.Entities;

namespace Domain.FileAggregate
{
    public class File : AuditableEntity
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }

        public User User { get; set; }
    }
}
