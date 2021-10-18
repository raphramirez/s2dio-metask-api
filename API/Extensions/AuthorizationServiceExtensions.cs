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

                // setting up policies for tasks
                options.AddPolicy("ReadAccess", policy =>
                    policy.RequireAssertion(context =>
                       context.User.HasClaim(claim =>
                         (claim.Type == "permissions" &&
                          (claim.Value == "read:tasks") &&
                          claim.Issuer == $"https://{config["Auth0:Domain"]}/"
                         )
                       )
                    )
                  );

                options.AddPolicy("WriteAccess", policy =>
                    policy.RequireAssertion(context =>
                       context.User.HasClaim(claim =>
                         (claim.Type == "permissions" &&
                          (claim.Value == "create:tasks" ||
                           claim.Value == "update:tasks") &&
                          claim.Issuer == $"https://{config["Auth0:Domain"]}/"
                         )
                       )
                    )
                  );

                options.AddPolicy("DeleteAccess", policy =>
                  policy.RequireAssertion(context =>
                     context.User.HasClaim(claim =>
                       (claim.Type == "permissions" &&
                        (claim.Value == "delete:tasks") &&
                        claim.Issuer == $"https://{config["Auth0:Domain"]}/"
                       )
                     )
                  )
                );

                // Organization
                options.AddPolicy("InviteAccess", policy =>
                    policy.RequireAssertion(context =>
                       context.User.HasClaim(claim =>
                         (claim.Type == "permissions" &&
                          (claim.Value == "invite:organization") &&
                          claim.Issuer == $"https://{config["Auth0:Domain"]}/"
                         )
                       )
                    )
                  );

                options.AddPolicy("ManageOrganizationAccess", policy =>
                    policy.RequireAssertion(context =>
                       context.User.HasClaim(claim =>
                         (claim.Type == "permissions" &&
                          (claim.Value == "manage:organization") &&
                          claim.Issuer == $"https://{config["Auth0:Domain"]}/"
                         )
                       )
                    )
                  );
            });

            services.AddTransient<IAuthorizationHandler, IsCreatorRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, IsAssigneeRequirementHandler>();

            return services;
        }
    }
}
