namespace MiniForum.Models
{
    public class CommentViewModel
    {
        public int CmtID { get; set; }
        public int PostID { get; set; }
        
        public string UserID { get; set; }
        public string Content { get; set; }
        public UserViewModel User { get; set; }
        
    }
}
