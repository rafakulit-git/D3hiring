using d3hiringNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Entity
{
    public class TeacherEntity : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(o => o.TeacherID);
            builder.Property(o => o.TeacherID);
            builder.Property(o => o.Name);
            builder.Property(o => o.Email);
        }
    }
}
