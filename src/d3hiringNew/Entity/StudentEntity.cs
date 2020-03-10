using d3hiringNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Entity
{
    public class StudentEntity : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(o => o.studentID);
            builder.Property(o => o.studentID);
            builder.Property(o => o.Name);
            builder.Property(o => o.Email);
        }
    }
}
