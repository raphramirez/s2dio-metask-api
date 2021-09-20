using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class IsCreatorRequirement : IAuthorizationRequirement
    {
    }
    public class IsCreatorRequirementHandler : AuthorizationHandler<IsCreatorRequirement>
    {
        private readonly PlutoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsCreatorRequirementHandler(PlutoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsCreatorRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Task.CompletedTask;

            var taskId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var task = _context.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == taskId).Result;

            if (task == null) return Task.CompletedTask;

            if (task.CreatedById == userId) context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }

}
