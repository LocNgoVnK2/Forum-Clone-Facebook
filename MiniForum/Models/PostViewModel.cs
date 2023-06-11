using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MiniForum.Models
{
    public class PostViewModel
    {
        public PostViewModel()
        {
        }

        [DisplayName("Post ID")]
        public int PostID { get; set; }

        [DisplayName("Caption")]
        public string Caption { get; set; }

        [DisplayName("Photo")]
        public IFormFile Photo { get; set; }

        public string UserID { get; set; }

        public int LikeCount { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IQueryable<FriendFollow> friendFollows { get; set; }

    }
}
