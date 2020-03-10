using d3hiringNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Entity
{
    public class NotificationEntity : IEntityTypeConfiguration<Notifications>
    {
        public void Configure(EntityTypeBuilder<Notifications> builder)
        {
            builder.HasKey(o => o.NotificationID);
            builder.Property(o => o.TeacherID).IsRequired();
            builder.Property(o => o.Notification).IsRequired();
            builder.Property(o => o.Recipients).IsRequired();
            builder.Property(o => o.ErrorRecords).IsRequired();
        }
    }
}
