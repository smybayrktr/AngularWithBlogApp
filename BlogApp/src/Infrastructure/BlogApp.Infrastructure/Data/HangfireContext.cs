using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Data
{
    public class HangfireContext : DbContext
    {
        public HangfireContext(DbContextOptions<HangfireContext> options) : base(options)
        {
        }
    }

}

