using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlogApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedDatabase(BlogAppContext context, IServiceProvider serviceProvider)
        {
            await seedUserIfNotExists(context, serviceProvider);
            seedCategoryIfNotExists(context);
            seedBlogIfNoExists(context);
            await seedAdminClaimIfNotExists(context, serviceProvider);
        }

        private static void seedCategoryIfNotExists(BlogAppContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new(){Name=".Net" },
                    new(){Name="Java" },

                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }
        private static void seedBlogIfNoExists(BlogAppContext context)
        {
            if (!context.Blogs.Any())
            {
                var blogs = new List<Blog>() {
                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı1?",
                                                           Body="Açıklama 1",
                                                           Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                           CategoryId=1,
                                                           Url=".net",
                                                           UserId=1 },

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı2?",
                                                         Body="Açıklama 2",
                                                         Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                         CategoryId=1,
                                                         Url=".",
                                                         UserId=1 },

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı3?",
                                                        Body="Açıklama yazısıdır bu",
                                                        Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                        CategoryId=2,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı4?",
                                                        Body=".net ile katmanlı mimari oluşturmak için yollar vardır",
                                                        Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                        CategoryId=1,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı5?",
                                                        Body="Bu yollar nelerdir",
                                                        Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                        CategoryId=2,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı6?",
                                                        Body="Onion arc ile farklı",
                                                        Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                        CategoryId=1,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı7?",
                                                        Body="Deneme",
                                                        Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                        CategoryId=2,
                                                        Url=".net",
                                                        UserId=1},

                        new() { Title=".Net ile Katmanlı Mimari Nasıl Olmalı8?",
                                                        Body=".net mimarisi",
                                                        Image="https://localhost:7227/images/uploaded-images/blog-1.jpg",
                                                        CategoryId=1,
                                                        Url=".net",
                                                        UserId=1},
                };

                context.Blogs.AddRange(blogs);
                context.SaveChanges();
            }

        }
        private static async Task seedUserIfNotExists(BlogAppContext context, IServiceProvider serviceProvider)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString()

                };
                var userManager = serviceProvider.GetService<UserManager<User>>();
                var passwordHasher = serviceProvider.GetService<IPasswordHasher<User>>();
                var hashedPassword = passwordHasher.HashPassword(user, "Admin123456:)");
                user.PasswordHash = hashedPassword;
                user.SecurityStamp = Guid.NewGuid().ToString();
                context.Users.Add(user);
                context.SaveChanges();

               
            }


        }
        private static async Task seedAdminClaimIfNotExists(BlogAppContext context, IServiceProvider serviceProvider)
        {
            var user = context.Users.First();
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var checkClaims = await userManager.GetClaimsAsync(user);
            if (checkClaims.Any())
            {
                return;
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            await userManager.AddClaimsAsync(user, claims).ConfigureAwait(false);
        }
    }
}

