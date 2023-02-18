using Microsoft.AspNetCore.Identity;

namespace UsingJWT.Models
{
    public class User : IdentityUser
    {
        public string  FirstName { get; set; }
    }
}
