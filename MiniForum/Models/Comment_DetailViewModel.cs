using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace MiniForum.Models
{
    public class Comment_DetailViewModel
    {
        public int PostID { get; set; }
        public int CmtID { get; set; }
        public string UserID { get; set; }
        public string Content { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }
        //
      

     

    }
}
