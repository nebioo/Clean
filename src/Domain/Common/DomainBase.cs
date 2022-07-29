using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

public abstract class DomainBase
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; protected set; }

    public void SetIsActive(bool value)
    {
        IsActive = value;
    }
}
