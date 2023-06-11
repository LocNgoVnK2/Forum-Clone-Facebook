using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Generic;
using Infrastructure.Entities;
using Infrastructure.EF;

namespace Infrastructure.Repository
{
    public interface ICommentRepository : IRepository<Comment>
    {
    }
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(EXDbContext context) : base(context)
        {
        }
    }
}
