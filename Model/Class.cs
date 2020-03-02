using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySql.Data.MySqlClient;

namespace d3hiring.Model
{
    [Table("tbl_class")]
    public class Class
    {
        [Key]
        public int classID { get; set; }

        public int TeacherID { get; set; }

        public int StudentID { get; set; }
    }

}
