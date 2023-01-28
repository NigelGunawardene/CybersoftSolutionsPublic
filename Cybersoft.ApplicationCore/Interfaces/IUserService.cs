using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> AddOrUpdateAsync(Users user);
        Task<UserModel> GetCurrentUserAsync(string userName);
        Task<UserModel> GetUserAsync(string userName);
        Task<UserModel> CheckUserCredentialsAsync(Users user);
        Task StoreRefreshTokenAsync(string username, string refreshToken);
        Task<Boolean> CheckIfUserHasValidTokenAsync(string username, string refreshToken);
        Task DeleteRefreshTokenAsync(string username);
        Task<PaginatedList<UserModel>> GetPaginatedUsersAsync(string accounttype, int pagesize, int pagenumber);

    }
}
