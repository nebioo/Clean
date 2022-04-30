using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class DomainBase
    {
        [Key]
        public Guid Id { get; set; }

        [Column(Order = 200)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 201)]
        public DateTime? ModifiedDate { get; set; }

        [Column(Order = 202)]
        public bool IsActive { get; protected set; }

        public void setIsActive(bool value)
        {
            IsActive = value;
        }
    }
}
