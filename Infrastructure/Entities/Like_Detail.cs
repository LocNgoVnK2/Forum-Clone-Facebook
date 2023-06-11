using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("Like_Detail")]
    public class Like_Detail
    {
        [Key]
        [Column(Order = 0)]
        public int PostID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserID { get; set; }


        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }
    }
}
