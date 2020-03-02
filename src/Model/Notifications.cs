using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiring.Model
{
    [Table("tbl_notification")]
    public class Notifications
    {
        [Key]
        public int NotificationID { get; set; }

        public int TeacherID { get; set; }

        public string Notification { get; set; }

        public string Recipients { get; set; }

    }
}
