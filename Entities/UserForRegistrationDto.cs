using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Entities.Shared
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(4)]
        [MaxLength(256)]
        public string Username { get; init; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        [MaxLength(400)]
        [DataType(DataType.Password)] public string Password { get; init; }
        
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; init; }
    }
}