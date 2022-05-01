using Domain.Common;

namespace Domain.TodoAggregate;

public class Todo : DomainBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}