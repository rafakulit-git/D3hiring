using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace d3hiring.Model
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
