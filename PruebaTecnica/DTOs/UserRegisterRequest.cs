using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTOs
{
    public class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
