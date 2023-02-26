
namespace GoGo.Idp.Application.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsRequireChangePassword { get; set; } = false;
        public IEnumerable<RoleItem?> Roles { get; set; } = Enumerable.Empty<RoleItem>();
        public IEnumerable<ClaimItem?> Claims { get; set; } = Enumerable.Empty<ClaimItem>();
    }

    public class RoleItem
    {
        public string? Name { get; set; }
    }

     public class ClaimItem
    {
        public string? Type { get; set; }
        public string? Value { get; set; }
    }
}