using Infrastructure.Entities;
using System.Linq;

namespace MiniForum.Models
{
    public class User_FollowViewModel
    {

        public string UserID1 { get; set; }
        public string UserID2 { get; set; }


        public IQueryable<FriendFollow> friendFollows { get; set; }
        public IQueryable<User> users { get; set; }
    }
}
