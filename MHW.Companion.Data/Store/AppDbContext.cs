using MHW.Companion.Model.Equipment;
using MHW.Companion.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MHW.Companion.Data.Store
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>, IAppDbContext
    {        
        public AppDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Weapon> Weapons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            InitEquipment(builder);
        }

        private void InitEquipment(ModelBuilder builder)
        {
            builder.Entity<Weapon>()
                .HasKey(x => x.Id);
        }

        public Task MigrateAsync()
        {
            return Database.MigrateAsync();
        }
    }
}
