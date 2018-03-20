using Microsoft.AspNetCore.Identity;

namespace MHW.Companion.Model.User
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
    }
}
