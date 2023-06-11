using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Repository;
namespace Infrastructure.Service
{
    public interface ICommentService
    {
        IQueryable<Comment> GetComments();
        Comment GetComment(int id);
        void InsertComment(Comment cmt);
        void UpdateComment(Comment cmt);
        void DeleteComment(Comment cmt);
    }
    public class CommentService : ICommentService
    {
        private ICommentRepository cmtRepository;

        public CommentService(ICommentRepository cmtRepository)
        {
            this.cmtRepository = cmtRepository;
        }
        public IQueryable<Comment> GetComments()
        {
            return cmtRepository.GetAll();
        }
        public Comment GetComment(int id)
        {
            return cmtRepository.GetById(id);
        }
        public void InsertComment(Comment cmt)
        {
            cmtRepository.Insert(cmt);
        }
        public void UpdateComment(Comment cmt)
        {
            cmtRepository.Update(cmt);
        }
        public void DeleteComment(Comment cmt)
        {
            cmtRepository.Delete(cmt);
        }

    }
}
