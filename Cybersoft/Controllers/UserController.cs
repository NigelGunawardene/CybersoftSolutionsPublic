using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Enums;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.ApplicationCore.Specifications;
using Cybersoft.Authentication;
using Cybersoft.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cybersoft.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : CommonApiController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IUserService _userService;
        private IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IUserService userService, IUnitOfWork unitOfWork, IJwtAuthenticationManager jwtAuthenticationManager, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<UserModel> AddUser([FromBody] Users model) //[FromBody] user //HttpRequest req
        {
            //string requestbody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            //var user = JsonSerializer.Deserialize<Users>(requestbody);
            //var enterUser = new Users()
            //{
            //    UserName = "Test User three",
            //    FullName = "Test User three",
            //    Email = "three@gmail.com",
            //    Password = "1qaz2wsx@",
            //    CurrentRole = ApplicationCore.Enums.UserRole.User,
            //    JoinedDate = DateTime.Now//2021-11-13 00:00:00.0000000
            //};
            //var enteredUser = await _unitOfWork.Users.AddAsync(enterUser);
            //_unitOfWork.Complete();

            //odel.JoinedDate = DateTime.Now;
            var enteredUser = await _userService.AddOrUpdateAsync(model);
            return enteredUser;
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("all/{accountType}")]
        [HttpGet]
        public async Task<List<UserModel>> GetUsers(string accountType)
        {
            if (accountType == "Customer")
            {
                var spec = new UserByRoleSpecification((UserRole)Enum.Parse(typeof(UserRole), accountType));
                var users = await _unitOfWork.Users.ListAsync(spec).ConfigureAwait(false); // CHANGE THIS TO GET ALL USERS. CAN CALL FOR CUSTOMER MANAGER PORTAL
                List<UserModel> listOfUserModels = new List<UserModel>();
                foreach (var user in users.ToList())
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
                return listOfUserModels;
            }
            else if (accountType == "All")
            {
                var users = await _unitOfWork.Users.ListAllAsync().ConfigureAwait(false);
                List<UserModel> listOfUserModels = new List<UserModel>();
                foreach (var user in users.ToList())
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
                return listOfUserModels;
            }
            else return null;
        }



        [Authorize(Roles = "Admin, SuperAdmin")]
        //[AllowAnonymous]
        [Route("paginated")] //[Route("all/{accountType}/{pagesize}/{pagenumber}")]
        [HttpGet]
        public async Task<IActionResult> GetPaginatedUsersAsync(string accountType, int pagesize, int pagenumber)
        {
            var users = await _userService.GetPaginatedUsersAsync(accountType.ToLower(), pagesize, pagenumber).ConfigureAwait(false);
            var metadata = new
            {
                users.TotalCount,
                users.PageSize,
                users.CurrentPage,
                users.TotalPages,
                users.HasNext,
                users.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("{username}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModel>> GetUser(string username)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(new UserByUserNameSpecification(username));
            //var user = await _userService.GetUserAsync("CyberMuffin").ConfigureAwait(false);
            var userModel = new UserModel()
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

        [Route("me")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userService.GetCurrentUserAsync(UserName).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound();
            }
            var userModel = new UserModel()
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
            return Ok(userModel);
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateUser([FromBody] Users model) //[FromBody] user //HttpRequest req
        {
            var loginUserPresent = await _userService.CheckUserCredentialsAsync(model);
            if (loginUserPresent == null) return NotFound();
            var tokenResponse = _jwtAuthenticationManager.Authenticate(true, loginUserPresent);
            if (tokenResponse == null) return Unauthorized();
            //JwtResponse response = new JwtResponse
            //{
            //    accessToken = token
            //};
            await _userService.StoreRefreshTokenAsync(loginUserPresent.UserName, tokenResponse.refreshToken).ConfigureAwait(false);
            return Ok(tokenResponse);
        }

        //[AllowAnonymous]
        [Route("revoke-token")]
        [HttpPost]
        public async Task<IActionResult> RevokeToken() //[FromBody] user //HttpRequest req
        {
            await Task.CompletedTask;
            //var loginUserStatus = await _userService.CheckUserCredentials(model);
            //if (!loginUserStatus) return Unauthorized();
            //var token = _jwtAuthenticationManager.Authenticate(loginUserStatus, model.UserName);
            //if (token == null) return Unauthorized();
            return Ok();
        }

        [AllowAnonymous]
        [Route("refresh-token")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCredentials credentials) //[FromBody] user //HttpRequest req
        {
            TokenRefreshHelper refresher = new TokenRefreshHelper(_configuration);
            try
            {
                // var credentialsJustInCase = Request.Headers.Authorization;
                var validUserTokenUsername = refresher.CheckIfTokenIsValid(credentials);
                var doesuserHaveValidTokenInDb = await _userService.CheckIfUserHasValidTokenAsync(validUserTokenUsername, credentials.refreshToken).ConfigureAwait(false);
                if (doesuserHaveValidTokenInDb)
                {
                    //await _userService.DeleteRefreshToken(validUserTokenUsername).ConfigureAwait(false);
                    if (validUserTokenUsername != null)
                    {
                        UserModel user = await _userService.GetUserAsync(validUserTokenUsername);
                        JwtResponse tokenResponse = _jwtAuthenticationManager.Authenticate(true, user);
                        await _userService.StoreRefreshTokenAsync(user.UserName, tokenResponse.refreshToken).ConfigureAwait(false);
                        return Ok(tokenResponse);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
                //else return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Route("checkusername/{username}")]
        [HttpGet]
        public async Task<IActionResult> CheckIfUsernameIsAvailable(string username)
        {
            var spec = new UserByUserNameSpecification(username);
            Users user = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
            if (user != null)
            {
                return Ok(false);
            }
            else return Ok(true);
        }

        [AllowAnonymous]
        [Route("checkemail/{email}")]
        [HttpGet]
        public async Task<IActionResult> CheckIfEmailIsAvailable(string email)
        {
            var spec = new UserByEmailSpecification(email);
            Users user = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
            if (user != null)
            {
                return Ok(false);
            }
            else return Ok(true);
        }

        [AllowAnonymous]
        [Route("checkphonenumber/{phonenumber}")]
        [HttpGet]
        public async Task<IActionResult> CheckIfPhonenumberIsAvailable(string phonenumber)
        {
            var spec = new UserByPhoneNumberSpecification(phonenumber);
            Users user = await _unitOfWork.Users.FirstOrDefaultAsync(spec).ConfigureAwait(false);
            if (user != null)
            {
                return Ok(false);
            }
            else return Ok(true);
        }

    }
}
