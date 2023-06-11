using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IFriendFollowService
    {
        IQueryable<FriendFollow> GetFriendFollows();
        FriendFollow GetFriendFollow(string userId1 , string userId2);
        void InsertFriendFollow(FriendFollow friendFollow);
        void UpdateFriendFollow(FriendFollow friendFollow);
        void DeleteFriendFollow(FriendFollow friendFollow);
    }
    public class FriendFollowService : IFriendFollowService
    {
        private IFriendFollowRepository friendFollowRepository;
        public FriendFollowService(IFriendFollowRepository friendFollowRepository)
        {
            this.friendFollowRepository = friendFollowRepository;
        }
        public void DeleteFriendFollow(FriendFollow friendFollow)
        {
            friendFollowRepository.Delete(friendFollow);
        }

        public FriendFollow GetFriendFollow(string userId1, string userId2)
        {
            return friendFollowRepository.GetById(userId1, userId2);
        }

        public IQueryable<FriendFollow> GetFriendFollows()
        {
            return friendFollowRepository.GetAll();
        }

        public void InsertFriendFollow(FriendFollow friendFollow)
        {
            friendFollowRepository.Insert(friendFollow);
        }

        public void UpdateFriendFollow(FriendFollow friendFollow)
        {
            friendFollowRepository.Update(friendFollow);
        }
    }
}
