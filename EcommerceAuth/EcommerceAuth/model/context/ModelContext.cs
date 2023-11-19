using EcommerceAuth.model.entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAuth.model.context
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){ }

        public DbSet<Users> Users { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<User_Rol> User_Rol { get; set; }

        public DbSet<ADirectory> ADirectory { get; set; }

        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DbSet<Funcionality> Funcionality { get; set; }

        public DbSet<Funcionality_Rol> Funcionality_Rol { get; set; }
    }
}
