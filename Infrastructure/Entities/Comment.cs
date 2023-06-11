using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
       
        public int CmtID { get; set; }
        [ForeignKey("UserID")]
        public string UserID { get; set; }
        [ForeignKey("PostID")]
        public int PostID { get; set; }
        public string Content  { get; set; }
    }
}
