using GoGo.Infrastructure.Repository;

namespace GoGo.Idp.Domain.Bases
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedUtc { get; set; }

    }
}