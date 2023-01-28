using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IUserRepository : IAsyncRepository<Users, int>
    {
        Task<Users> GetUser(string userName);



        //Task<Users> CreateUser(Users user);
    }
}
