using System.Text;
using System.Text.Json;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.Helpers.EmailHelper;
using BlogApp.Core.Utilities.Helpers.FileHelper;
using BlogApp.Core.Utilities.Helpers.UrlHelper;
using BlogApp.Core.Utilities.Jwt;
using BlogApp.Core.Utilities.Results;
using BlogApp.Infrastructure.Data;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.EntityFramework;
using BlogApp.Services.Repositories.AppUser;
using BlogApp.Services.Repositories.Auth;
using BlogApp.Services.Repositories.Blog;
using BlogApp.Services.Repositories.Category;
using BlogApp.Services.Repositories.Email;
using BlogApp.Services.Repositories.SavedBlogAction;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.WebApi.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddInjections(this IServiceCollection services, IConfiguration configuration)
        {
            //uygulama build olmadan önce applicationun kullanacağı nesneleri containere eklememiz lazım.
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogRepository, EfBlogRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, EfUserRepository>();

            services.AddScoped<ISavedBlogService, SavedBlogService>();
            services.AddScoped<ISavedBlogRepository, EfSavedBlogRepository>();

            services.AddScoped<IFileHelper, FileHelper>();
            services.AddScoped<IUrlHelper, UrlHelper>();
            services.AddScoped<IEmailService, EmailService>();


            services.AddScoped<IAuthService, AuthService>();
            services.AddDbContext<BlogAppContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("BlogApp")), ServiceLifetime.Transient);
            services.AddDbContext<HangfireContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("Hangfire")), ServiceLifetime.Transient);

            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };
                config.UseSqlServerStorage(configuration.GetConnectionString("Hangfire"), option)
                      .WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });
            services.AddHangfireServer();

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 7 });

            var emailConfig = configuration.GetSection("EmailConfiguration")
                                           .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            var tokenOption = configuration.GetSection("TokenOption").Get<TokenOption>();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = tokenOption.Issuer,
                        ValidateIssuer = true,
                        ValidAudience = tokenOption.Audience,
                        ValidateAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey)),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.Response.OnStarting(async () =>
                            {
                                context.Response.StatusCode = ApiStatusCodes.Unauthorized;
                                var response = new ErrorResult(Messages.Unauthorized, 401);
                                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
                            });

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}

