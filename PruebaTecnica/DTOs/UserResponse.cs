using PruebaTecnica.Entities;

namespace PruebaTecnica.DTOs
{
    public class UserResponse
    {
        public string Username { get; set; } = String.Empty;

        public UserResponse() { }
        public UserResponse(User user) 
        {
            Username = user.Username;  
        }
    }
}
