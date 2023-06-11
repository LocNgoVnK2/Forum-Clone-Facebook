using System.Linq;

namespace MiniForum.Models
{
    public class Post_CommentViewModel
    {
        public PostViewModel postViewModel { get; set; }    
        public CommentViewModel commentViewModel { get; set; }
    }
}
