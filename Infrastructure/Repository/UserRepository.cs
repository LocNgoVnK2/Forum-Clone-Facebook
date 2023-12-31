﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Generic;
using Infrastructure.Entities;
using Infrastructure.EF;

namespace Infrastructure.Repository
{
    public interface IUserRepository : IRepository<User>
    {
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EXDbContext context) : base(context)
        {
        }
    }
}
