using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Models
{
    [Table("tbl_notification")]
    public class Notifications
    {
        public int NotificationID { get; set; }

        public int TeacherID { get; set; }

        public string Notification { get; set; }

        public string Recipients { get; set; }

        public string ErrorRecords { get; set; }

    }
}
