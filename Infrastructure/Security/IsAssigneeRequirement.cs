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
    public class IsAssigneeRequirement : IAuthorizationRequirement
    {
    }
    public class IsAssigneeRequirementHandler : AuthorizationHandler<IsAssigneeRequirement>
    {
        private readonly PlutoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsAssigneeRequirementHandler(PlutoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAssigneeRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Task.CompletedTask;

            var taskId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var task = _context.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == taskId).Result;

            if (task == null) return Task.CompletedTask;

            if (task.AssigneeId == userId) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
