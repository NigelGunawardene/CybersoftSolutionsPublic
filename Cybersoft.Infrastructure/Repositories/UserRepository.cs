using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.Infrastructure.Repositories
{
    public class UserRepository : EfRepository<Users, int>, IUserRepository
    {
        private readonly CyberSoftContext _dbContext;

        public UserRepository(CyberSoftContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Users> GetUser(string userName)
        {
            return (Task<Users>)_dbContext.Users.Where(x => x.UserName == userName);
        }
    }
}
