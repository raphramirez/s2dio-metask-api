using API.Extensions;
using API.Middleware;
using Application.Notifications;
using Application.Tasks;
using Domain.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Repositories;
using System.Collections.Generic;
using System.Net;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<Create>();
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = new List<string>();
                    foreach (var state in context.ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }

                    var apiErrorResponse = new
                    {
                        Title = "One or more validation errors occured.",
                        Instance = context.HttpContext.Request.Path.Value,
                        Status = (int)HttpStatusCode.BadRequest,
                        Errors = errors
                    };

                    return new BadRequestObjectResult(apiErrorResponse);
                };
            });
            services.AddApplicationServices(_config);
            services.AddAuthenticationWithAuth0(_config);
            services.AddPolicies(_config);

            services.AddSingleton<FirebaseNotificationService>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<INotificationTokenRepository, NotificationTokenRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
