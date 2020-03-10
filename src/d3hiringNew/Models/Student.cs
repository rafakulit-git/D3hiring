using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace d3hiringNew.Models
{
    [Table("tbl_student")]
    public class Student
    {
        [Key]
        public int studentID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Suspended { get; set; }
    }
}
