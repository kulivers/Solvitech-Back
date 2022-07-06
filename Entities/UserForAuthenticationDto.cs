using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Entities.Shared
{
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email name is required")]
        public string Email { get; init; }
        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; init; }
    }
}