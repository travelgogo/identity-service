using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoGo.Idp.Domain.Bases;

namespace GoGo.Idp.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsRequireChangePassword { get; set; } = false;
    }
}