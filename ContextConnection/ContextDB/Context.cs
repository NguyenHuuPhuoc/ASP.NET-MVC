using DOMAIN.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ContextConnection.ContextDB
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context() : base("ConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Unit> Units { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<Meterial> Meterials { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> ApplicationRoles { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public static Context Create()
        {
            return new Context();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>().HasKey(n => new { n.UserId, n.RoleId }).ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(n => n.UserId).ToTable("ApplicationUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(n => n.UserId).ToTable("ApplicationUserClaims");
        }
    }
}