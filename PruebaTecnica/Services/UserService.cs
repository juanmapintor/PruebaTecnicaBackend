using PruebaTecnica.Entities;
using PruebaTecnica.Helpers;
using PruebaTecnica.Repository;

namespace PruebaTecnica.Services
{
    public interface IUserService
    {
        User? Authenticate(string  username, string password);
        User? Register(string username, string password);
        bool Exists(string username);
    }
    public class UserService : IUserService
    {
        private readonly PruebaTecnicaContext _context;
        public UserService(PruebaTecnicaContext context) 
        { 
            _context = context;
        }
        public User? Authenticate(string username, string password)
        {
            return _context.Users.Where(user => user.Username == username && user.Password == SHA256Helper.GetSHA256(password)).FirstOrDefault();
        }

        public User? Register(string username, string password)
        {
            if (username == String.Empty || password == String.Empty || Exists(username)) return null;

            var newUser = new User()
            {
                Username = username,
                Password = SHA256Helper.GetSHA256(password)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public bool Exists(string username)
        {
            return _context.Users.Where(user => user.Username == username).Any();
        }
    }
}
