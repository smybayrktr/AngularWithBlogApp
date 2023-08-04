using Microsoft.AspNetCore.Identity;

namespace BlogApp.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public bool Status { get; set; } = true;
    }
}

