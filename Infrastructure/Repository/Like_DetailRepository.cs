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
    public interface ILike_DetailRepository : IRepository<Like_Detail>
    {
    }
    public class Like_DetailRepository : Repository<Like_Detail>, ILike_DetailRepository
    {
        public Like_DetailRepository(EXDbContext context) : base(context)
        {
        }
    }
}
