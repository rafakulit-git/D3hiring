using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Models
{
    [Table("tbl_class")]
    public class Class
    {
        public int classID { get; set; }

        public int TeacherID { get; set; }

        public int StudentID { get; set; }
    }
}
