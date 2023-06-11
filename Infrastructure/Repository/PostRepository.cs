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
    public interface IPostRepository: IRepository<Post>
    {
    }
    public class PostRepository: Repository<Post>, IPostRepository
    {
        public PostRepository(EXDbContext context) : base(context)
        {
        }
    }
}
