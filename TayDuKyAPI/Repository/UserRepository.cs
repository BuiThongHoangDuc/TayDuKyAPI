using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Enums;
using TayDuKyAPI.Models;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PRM391Context _context;
        public UserRepository(PRM391Context context)
        {
            _context = context;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        public IQueryable<LoginViewModel> CheckLogin(string userEmail, string password)
        {
            var loginInfo = _context.Users
                                .Where(us => us.UserEmail.Equals(userEmail) && us.UserPassword.Equals(password) && us.UserStatus.Equals(Status.AVAILABLE))
                                .Select(us => new LoginViewModel
                                {
                                    UserId = us.UserId,
                                    UserName = us.UserName,
                                    UserEmail = us.UserEmail,
                                    UserImage = us.UserImage,
                                    UserRole = us.UserRole,
                                });
            return loginInfo;
        }

    }

    public interface IUserRepository
    {
        IQueryable<LoginViewModel> CheckLogin(string userEmail, string password);
    }
}
