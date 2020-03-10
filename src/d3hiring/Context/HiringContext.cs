
using d3hiring.Model;
using Microsoft.EntityFrameworkCore;

namespace d3hiring.Context
{
    public class HiringContext : DbContext
    {
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Notifications> Notifications { get; set; }


        public HiringContext(DbContextOptions<HiringContext> options)
            : base(options) { }
    }
}
