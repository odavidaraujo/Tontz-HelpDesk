using System.ComponentModel.DataAnnotations;

namespace Tontz_frontend.Models {
    public class LoginViewModel {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
