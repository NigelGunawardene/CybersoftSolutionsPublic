using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Enums;
using Cybersoft.ApplicationCore.Helpers;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Services
{
    public class UserService : IUserService
    {

        //private readonly IAsyncRepository<Users, int> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private const string DomainName = "";

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel> AddOrUpdateAsync(Users user)
        {
            ValidateUser(user);
            await ProcessUserAsync(user);
            var userModel = new UserModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                JoinedDate = user.JoinedDate,
            };
            return userModel;
        }

        private async Task<Users> ProcessUserAsync(Users user)
        {
            user.Role = ApplicationCore.Enums.UserRole.Customer;

            user.UserName = RemoveWhitespace(user.UserName);
            user.Email = RemoveWhitespace(user.Email);
            user.PhoneNumber = RemoveWhitespace(user.PhoneNumber);
            user.Password = RemoveWhitespace(user.Password);

            await AddUserAsync(user); //.ConfigureAwait(false)
            return user;
        }

        private async Task<Users> AddUserAsync(Users user)
        {
            EncryptUser(user);
            await _unitOfWork.Users.AddAsync(user);
            //_unitOfWork.Complete();
            return user;
        }


        private void EncryptUser(Users user)
        {
            // Hash
            var hashedPassword = SecurePasswordHasher.Hash(user.Password);
            // PASSWORD HASHING HERE
            user.Password = hashedPassword;
        }

        private void ValidateUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(userName));
            }
        }

        private void ValidateUser(Users user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Users.UserName));
            }
            if (string.IsNullOrEmpty(user.FirstName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Users.FirstName));
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Users.LastName));
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Users.Email));
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Users.Password));
            }
            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(Users.PhoneNumber));
            }
        }

        public async Task DeleteAsync(string userName)
        {
            ValidateUserName(userName);
            var doesUserExist = await IsExistingUser(userName);

            if (!doesUserExist)
            {
                var existingUser = await GetUserInOrderToDeleteAsync(userName);
                await _unitOfWork.Users.DeleteAsync(existingUser).ConfigureAwait(false);
            }
        }

        public async Task<UserModel> GetUserAsync(string userName)
        {
            var specification = new UserByUserNameSpecification(userName);
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(specification); //.ConfigureAwait(false)
            var userModel = new UserModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                JoinedDate = user.JoinedDate
            };
            return userModel;
        }

        public async Task<Users> GetUserInOrderToDeleteAsync(string userName)
        {
            var specification = new UserByUserNameSpecification(userName);
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(specification); //.ConfigureAwait(false)
            return user;
        }

        private async Task<bool> IsExistingUser(string userName)
        {
            var specification = new UserByUserNameSpecification(userName);
            int userCount = await _unitOfWork.Users.CountAsync(specification).ConfigureAwait(false);
            if (userCount != 0)
            {
                return userCount == 1;
            }
            return userCount == 0;
        }

        public async Task<UserModel> GetCurrentUserAsync(string userName)
        {
            var spec = new UserByUserNameSpecification(userName);
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
            if (user == null)
            {
                return null;
            }
            var userModel = new UserModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                JoinedDate = user.JoinedDate
            };
            return userModel;
        }

        //public async Task<Users> CheckUserCredentials(Users loggingInUser)
        //{

        //    // Verify
        //    //var result = SecurePasswordHasher.Verify(loggingInUser, hash);
        //    var spec = new UserByUserNameAndPasswordSpecification(loggingInUser.UserName, loggingInUser.Password);
        //    var loginUser = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
        //    if (loginUser == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return loginUser;
        //    }
        //}

        public async Task<UserModel> CheckUserCredentialsAsync(Users loggingInUser)
        {

            var spec = new UserByUserNameSpecification(loggingInUser.UserName);
            var loginUserWithUsername = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
            var specEmail = new UserByEmailSpecification(loggingInUser.UserName);
            var loginUserWithEmail = await _unitOfWork.Users.FirstOrDefaultAsync(specEmail).ConfigureAwait(false);
            if (loginUserWithUsername == null && loginUserWithEmail == null)
            {
                return null;
            }
            else
            {
                var loginUser = new Users();
                if (loginUserWithUsername != null)
                {
                    loginUser = loginUserWithUsername;
                }
                else
                {
                    loginUser = loginUserWithEmail;
                }
                // Verify Hash
                var result = SecurePasswordHasher.Verify(loggingInUser.Password, loginUser.Password);
                if (result)
                {
                    var loginUserModel = new UserModel
                    {
                        UserName = loginUser.UserName,
                        FirstName = loginUser.FirstName,
                        LastName = loginUser.LastName,
                        FullName = loginUser.FullName,
                        Email = loginUser.Email,
                        PhoneNumber = loginUser.PhoneNumber,
                        Role = loginUser.Role,
                        JoinedDate = loginUser.JoinedDate
                    };
                    return loginUserModel;
                }
                else return null;
            }
        }

        public async Task StoreRefreshTokenAsync(string username, string refreshToken)
        {
            var spec = new UserByUserNameSpecification(username);
            var updateUser = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
            updateUser.RefreshToken = refreshToken;
            updateUser.RefreshTokenAddedTime = DateTime.UtcNow;
            await _unitOfWork.Users.UpdateAsync(updateUser).ConfigureAwait(false);
        }

        public async Task<bool> CheckIfUserHasValidTokenAsync(string username, string refreshToken)
        {
            var spec = new UserByUserNameAndRefreshTokenSpecification(username, refreshToken);
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false); // AND token is less than 7 days old
            if (user != null)
            {
                return true;
            }
            else return false;
        }

        public async Task DeleteRefreshTokenAsync(string username)
        {
            var spec = new UserByUserNameSpecification(username);
            var updateUser = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
            updateUser.RefreshToken = null;
            await _unitOfWork.Users.UpdateAsync(updateUser).ConfigureAwait(false);
        }

        private static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public async Task<PaginatedList<UserModel>> GetPaginatedUsersAsync(string accounttype, int pagesize, int pagenumber)
        {
            UserRole userRole = DetermineUserRole(accounttype);
            //var parameters = new PaginationParams(pagesize, pagenumber);
            //var users = await _unitOfWork.Users.GetPaginatedItemsWithOrderByDescendingIdAsync(parameters);
            //var users = await _unitOfWork.Users.GetPaginatedItemsWithFilterAsync(x => x.Role == userRole, orderBy: x => x.OrderByDescending(x => x.Id), first: pagesize, offset: pagenumber);
            var specification = new PaginatedUsersSpecification(pagesize, pagenumber);
            var users = await _unitOfWork.Users.ListAsync(specification).ConfigureAwait(false);
            List<UserModel> listOfUserModels = new List<UserModel>();
            foreach (var user in users)
            {
                listOfUserModels.Add(new UserModel
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    JoinedDate = user.JoinedDate
                });
            }
            //var paginatedResponse = PaginatedList<UserModel>.ToPagedList(listOfUserModels.AsQueryable(), pagenumber, pagesize);
            var totalCustomersSpecification = new UsersTotalCountSpecification();
            var totalItems = await _unitOfWork.Users.CountAsync(totalCustomersSpecification).ConfigureAwait(false);
            //paginatedResponse.TotalCount = totalItems;
            //paginatedResponse.TotalPages = (int)Math.Ceiling(totalItems / (double)paginatedResponse.PageSize);
            var paginatedResponse = PaginatedList<UserModel>.ToCustomPagedList(listOfUserModels.AsQueryable(), totalItems, pagenumber, pagesize);
            return paginatedResponse;
        }

        public UserRole DetermineUserRole(string accountType)
        {
            //if (accountType.Equals("admin"))
            //{
            //    return UserRole.Admin;
            //}
            //else
            //{
            //    return UserRole.Customer;
            //}
            return UserRole.Customer;
        }

    }
}
