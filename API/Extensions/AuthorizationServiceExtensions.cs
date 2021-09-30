using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class AuthorizationServiceExtensions
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services, IConfiguration config)
        {
            string domain = $"https://{config["Auth0:Domain"]}/";

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsCreator", policy =>
                {
                    policy.Requirements.Add(new IsCreatorRequirement());
                });
                options.AddPolicy("IsAssignee", policy =>
                {
                    policy.Requirements.Add(new IsAssigneeRequirement());
                });
                options.AddPolicy("read:tasks", policy => policy.Requirements.Add(new HasScopeRequirement("read:tasks", domain)));
            });

            services.AddTransient<IAuthorizationHandler, IsCreatorRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, IsAssigneeRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, HasScopeRequirementHandler>();

            return services;
        }
    }
}
