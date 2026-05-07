using SmartWholesale.DAL;
using SmartWholesale.Models;

namespace SmartWholesale.BLL
{
    public class AuthService
    {
        private readonly UserRepository _userRepo;

        public AuthService()
        {
            _userRepo = new UserRepository();
        }

        public UserBase? Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            return _userRepo.Login(email, password);
        }

        public bool Register(UserBase user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UPassword))
                return false;

            return _userRepo.AddUser(user);
        }
    }
}
