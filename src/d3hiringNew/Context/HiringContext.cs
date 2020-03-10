using d3hiringNew.Entity;
using d3hiringNew.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Context
{
    public class HiringContext : DbContext
    {
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Notifications> Notifications { get; set; }


        public HiringContext(DbContextOptions<HiringContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeacherEntity());
            modelBuilder.ApplyConfiguration(new StudentEntity());
            modelBuilder.ApplyConfiguration(new NotificationEntity());
            modelBuilder.ApplyConfiguration(new ClassEntity());

        }
    }
}
