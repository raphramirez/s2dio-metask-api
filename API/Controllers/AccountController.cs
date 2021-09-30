using AutoMapper;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationTokenRepository _notificationTokenRepository;
        private readonly IMapper _mapper;

        public AccountController(IUserRepository userRepository, INotificationTokenRepository notificationTokenRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _notificationTokenRepository = notificationTokenRepository;
        }
    }
}