using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Infrastructure.Entities
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        public string Caption { get; set; }

        public  byte[] Photo { get; set; }

        public string UserID { get; set; }
        // tạo khóa phụ

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [NotMapped] // Không ánh xạ vào CSDL
        public int LikeCount { get; set; }
        
        [NotMapped]
        public List<string> LikedUserIDs { get; set; }
    }
}
