using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Repository;
namespace Infrastructure.Service
{
    public interface IPostService
    {
        IQueryable<Post> GetPosts();
        Post GetPost(int id);
        void InsertPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
    }
    public class PostService: IPostService
    {
        private IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        public IQueryable<Post> GetPosts()
        {
            return postRepository.GetAll();
        }
        public Post GetPost(int id)
        {
            return postRepository.GetById(id);
        }
        public void InsertPost(Post post)
        {
            postRepository.Insert(post);
        }
        public void UpdatePost(Post post)
        {
            postRepository.Update(post);
        }
        public void DeletePost(Post post)
        {
            postRepository.Delete(post);
        }

    }
}
