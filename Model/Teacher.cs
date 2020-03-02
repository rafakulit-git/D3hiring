using System.ComponentModel.DataAnnotations.Schema;

namespace d3hiring.Model
{
    [Table("tbl_teacher")]
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

    }
}