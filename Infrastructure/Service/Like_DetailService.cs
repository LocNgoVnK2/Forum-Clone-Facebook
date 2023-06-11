using System.Linq;
using Infrastructure.Entities;
using Infrastructure.Repository;
namespace Infrastructure.Service
{
    public interface ILike_DetailService
    {
        IQueryable<Like_Detail> GetLike_Details();
        Like_Detail GetLike_Detail(int postId, string userId);
        void InsertLike_Detail(Like_Detail like_detail);
        void UpdateLike_Detail(Like_Detail like_detail);
        void DeleteLike_Detail(Like_Detail like_detail);
       
        int CountLikeForEachPost(int postId);
    }
    public class Like_DetailService : ILike_DetailService
    {
        private ILike_DetailRepository like_detailRepository;

        public Like_DetailService(ILike_DetailRepository like_detailRepository)
        {
            this.like_detailRepository = like_detailRepository;
        }
        public IQueryable<Like_Detail> GetLike_Details()
        {
            return like_detailRepository.GetAll();
        }
        public Like_Detail GetLike_Detail(int postId, string userId)
        {
            return like_detailRepository.GetById(postId, userId);
        }
        public void InsertLike_Detail(Like_Detail like_detail)
        {
            like_detailRepository.Insert(like_detail);
        }
        public void UpdateLike_Detail(Like_Detail like_detail)
        {
            like_detailRepository.Update(like_detail);
        }
        public void DeleteLike_Detail(Like_Detail like_detail)
        {
            like_detailRepository.Delete(like_detail);
        }

        public int CountLikeForEachPost(int postId)
        {
            var likes = like_detailRepository.GetAll().Where(l=>l.PostID == postId);
            return likes.Count();
        }

    }
}




