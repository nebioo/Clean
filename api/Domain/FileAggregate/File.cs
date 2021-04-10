using Domain.Common;

namespace Domain.FileAggregate
{
    public class File : AuditableEntity
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }
    }
}
