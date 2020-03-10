using d3hiringNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Entity
{
    public class ClassEntity : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasKey(o => o.classID);
            builder.Property(o => o.TeacherID).IsRequired();
            builder.Property(o => o.StudentID).IsRequired();
        }
    }
}
