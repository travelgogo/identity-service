using GoGo.Infrastructure.Repository;

namespace GoGo.Idp.Domain.Entities
{
    public class UserClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public User? User { get; set; }
    }
}