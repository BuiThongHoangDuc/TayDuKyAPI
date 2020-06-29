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
                                .Where(us => us.UserEmail.Equals(userEmail) && us.UserPassword.Equals(password) && us.UserIsDelete.Equals(IsDelete.ACTIVE))
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

        public IQueryable<ActorBasicInfoVM> GetListActor()
        {
            var listActor = _context.Users
                                    .Where(us => us.UserRole.Equals(Role.USER) && us.UserIsDelete.Equals(IsDelete.ACTIVE))
                                    .Select(us => new ActorBasicInfoVM
                                    {
                                        UserId = us.UserId,
                                        UserEmail = us.UserEmail,
                                        UserImage = us.UserImage,
                                        UserName = us.UserName,
                                    });
            return listActor;
        }

        public IQueryable<ActorBasicInfoVM> SearchActor(string userName)
        {
            var listActor = _context.Users
                                    .Where(us => us.UserName.Contains(userName) && us.UserRole.Equals(Role.USER) && us.UserIsDelete.Equals(IsDelete.ACTIVE))
                                    .Select(us => new ActorBasicInfoVM
                                    {
                                        UserId = us.UserId,
                                        UserEmail = us.UserEmail,
                                        UserImage = us.UserImage,
                                        UserName = us.UserName,
                                    });
            return listActor;
        }
    }

    public interface IUserRepository
    {
        IQueryable<LoginViewModel> CheckLogin(string userEmail, string password);
        IQueryable<ActorBasicInfoVM> GetListActor();
        IQueryable<ActorBasicInfoVM> SearchActor(string userName);
    }
}
