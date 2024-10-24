using Microsoft.EntityFrameworkCore;
using Operations.API.Domain.Entities;

namespace Operations.API.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
