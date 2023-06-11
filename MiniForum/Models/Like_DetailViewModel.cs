using System.ComponentModel;

namespace MiniForum.Models
{
    public class Like_DetailViewModel
    {
        public Like_DetailViewModel()
        {
        }

        [DisplayName("Post ID")]
        public int PostID { get; set; }

        [DisplayName("Caption")]
        public string UserID { get; set; }
    }
}
