using System.ComponentModel.DataAnnotations;
using AspNetCore.Identity.Mongo.Model;

namespace VisumAPI.Models
{
    public class Customer : MongoUser
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }

    public class UserRole : MongoRole
    {

    }
}
