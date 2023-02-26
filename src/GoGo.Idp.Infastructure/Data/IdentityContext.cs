using GoGo.Idp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoGo.Idp.Infastructure.Data
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration<User>(UserConfiguration);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<UserClaim>? UserClaims { get; set; }
    }
}