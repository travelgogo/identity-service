using GoGo.Infrastructure.Repository;

namespace GoGo.Idp.Domain.Entities
{
    public class UserRole : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public Role? Role { get; set; }
    }
}