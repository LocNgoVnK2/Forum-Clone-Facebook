using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IFriendFollowRepository : IRepository<FriendFollow>
    {
    }
    public class FriendFollowRepository : Repository<FriendFollow>, IFriendFollowRepository
    {
        public FriendFollowRepository(EXDbContext context) : base(context)
        {
        }
    }
}
