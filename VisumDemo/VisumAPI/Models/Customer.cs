using System.ComponentModel.DataAnnotations;

namespace VisumAPI.Models
{
    public class Customer { 
        public string UserName { get; set; }

    }

    public class AuthResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
