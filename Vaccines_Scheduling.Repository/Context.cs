using Microsoft.EntityFrameworkCore;
using Vaccines_Scheduling.Entity.Entities;

namespace Vaccines_Scheduling.Repository
{
    public class Context : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
