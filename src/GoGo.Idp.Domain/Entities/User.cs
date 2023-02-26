using GoGo.Idp.Domain.Bases;

namespace GoGo.Idp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsRequireChangePassword { get; set; } = false;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

    }
}