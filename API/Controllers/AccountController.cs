using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Application.Notifications;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService, DataContext context, IMapper mapper)
        {
            _tokenService = tokenService;
            _context = context;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<List<Application.Profiles.Profile>> GetAppUsers()
        {
            var users = await _context.AppUsers.ToListAsync();

            var usersToReturn = _mapper.Map<List<Application.Profiles.Profile>>(users);

            return usersToReturn;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user),
                    Image = null,
                };
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                var apiErrorResponse = new
                {
                    Title = "One or more validation errors occured",
                    Instance = "/api/account/register",
                    Status = (int)HttpStatusCode.BadRequest,
                    Errors = new string[]
                    {
                        "Username is already taken."
                    }
                };

                return BadRequest(apiErrorResponse);
            }

            var user = new AppUser
            {
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return BadRequest(new
            {
                Title = "One or more validation errors occured",
                Instance = "/api/account/register",
                Status = (int)HttpStatusCode.BadRequest,
                Errors = new string[]
                    {
                        "Failed to register the user."
                    }
            });
        }

        [Authorize]
        [HttpPost("password/change")]
        public async Task<ActionResult<Unit>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return Unauthorized();

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Ok(Unit.Value);
            } 
            else
            {
                var apiErrorResponse = new
                {
                    Title = "One or more validation errors occured",
                    Instance = "/api/account/register",
                    Status = (int)HttpStatusCode.BadRequest,
                    Errors = changePasswordResult.Errors.Select(x => x.Description)
                };

                return BadRequest(apiErrorResponse);
            }
        }

        [HttpGet("tokens")]
        [Authorize]
        public async Task<ActionResult<List<NotificationTokenDto>>> UserTokens(UsernameDto usernameDto)
        {
            var user = await _userManager.FindByNameAsync(usernameDto.Username);

            if (user == null) return NotFound();

            var tokens = await _context.NotificationTokens
                  .Include(a => a.AppUser)
                  .Where(t => t.AppUser.UserName == usernameDto.Username)
                  .ToListAsync();

            var tokensToReturn = _mapper.Map<List<NotificationTokenDto>>(tokens);

            return tokensToReturn;
        }

        [HttpPost("tokens/register")]
        [Authorize]
        public async Task<ActionResult<Unit>> RegisterToken(RegisterTokenDto tokenDto)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null) return Unauthorized();

            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return Unauthorized();

            var duplicateToken = await _context.NotificationTokens
                .FirstOrDefaultAsync(t => t.AppUser.UserName == user.UserName && t.Value == tokenDto.Token);
            if (duplicateToken != null)
            {
                return Unit.Value;
            }

            user.Tokens.Add(new NotificationToken { Value = tokenDto.Token });

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                var apiErrorResponse = new
                {
                    Title = "One or more validation errors occured",
                    Instance = "/api/account/tokens/register",
                    Status = (int)HttpStatusCode.BadRequest,
                    Errors = new string[]
                    {
                        "Failed to register token."
                    }
                };

                return BadRequest(apiErrorResponse);
            }

            return Unit.Value;
        }

        [HttpDelete("tokens/delete")]
        [Authorize]
        public async Task<ActionResult<Unit>> DeleteToken(RegisterTokenDto tokenDto)
        {
            var token = await _context.NotificationTokens.FirstOrDefaultAsync(x => x.Value == tokenDto.Token);

            if (token == null) return null;

            _context.Remove(token);

            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return BadRequest("Failed to delte token.");

            return Unit.Value;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            if (username == null)
            {
                var apiErrorResponse = new
                {
                    Title = "One or more validation errors occured",
                    Instance = "/api/account",
                    Status = (int)HttpStatusCode.BadRequest,
                    Errors = new string[]
                    {
                        "The token is either invalid or expired."
                    }
                };

                return BadRequest(apiErrorResponse);
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                Token = _tokenService.CreateToken(user),
                Username = user.UserName,
            };
        }
    }
}