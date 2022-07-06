using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solvintech.Entities
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(40)] public string Username { get; set; }
        [EmailAddress] public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Token { get; set; }
    }
}