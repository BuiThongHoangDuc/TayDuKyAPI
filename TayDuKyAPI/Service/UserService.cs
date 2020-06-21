using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TayDuKyAPI.Repository;
using TayDuKyAPI.ViewModel;

namespace TayDuKyAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;

        public UserService(IUserRepository user)
        {
            _user = user;
        }

        public IQueryable<LoginViewModel> CheckLoginSV(string userEmail, string password)
        {
            return _user.CheckLogin(userEmail, password);
        }
    }

    public interface IUserService
    {
        IQueryable<LoginViewModel> CheckLoginSV(string userEmail, string password);
    }
}
