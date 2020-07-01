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

        public async Task AddActorSV(ActorInfoVM actor)
        {
            await _user.AddActor(actor);
        }

        public IQueryable<LoginViewModel> CheckLoginSV(string userEmail, string password)
        {
            return _user.CheckLogin(userEmail, password);
        }

        public IQueryable<ActorBasicInfoVM> GetListActorVM()
        {
            return _user.GetListActor();
        }

        public IQueryable<ActorBasicInfoVM> SearchActorVM(string userName)
        {
            return _user.SearchActor(userName.Trim());
        }
    }

    public interface IUserService
    {
        IQueryable<LoginViewModel> CheckLoginSV(string userEmail, string password);
        IQueryable<ActorBasicInfoVM> GetListActorVM();
        IQueryable<ActorBasicInfoVM> SearchActorVM(string userName);
        Task AddActorSV(ActorInfoVM actor);

    }
}
