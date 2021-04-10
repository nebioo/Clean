using System.Collections.Generic;
using Domain.Common;
using Domain.FileAggregate;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        private ICollection<File> Files { get; set; }
    }
}
