using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTOs;
using PruebaTecnica.Entities;
using PruebaTecnica.Helpers;
using PruebaTecnica.Repository;

namespace PruebaTecnica.Services
{
    public interface IUserService
    {
        User? Authenticate(string  username, string password);
        ActionResult<UserResponse> Register(UserRegisterRequest request);
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
            return _context.Users
                .Where(
                user => 
                    user.Username == username && 
                    user.Password == SHA256Helper.GetSHA256(password)
                ).FirstOrDefault();
        }

        public ActionResult<UserResponse> Register(UserRegisterRequest request)
        { 
            if (request == null || 
            request.Username == String.Empty || 
            request.Password == String.Empty || 
            _context.Users.Where(user => user.Username == request.Username).Any()) 
                return new UnprocessableEntityResult();

            var newUser = new User()
            {
                Username = request.Username,
                Password = SHA256Helper.GetSHA256(request.Password)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new UserResponse(newUser);
        }

    }}
