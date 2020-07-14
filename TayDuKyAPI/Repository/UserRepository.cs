using Microsoft.EntityFrameworkCore;
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

        public async Task AddActor(ActorInfoVM actor)
        {
            User userModel = new User();
            userModel.UserName = actor.UserName;
            userModel.UserPassword = actor.UserPassword;
            userModel.UserImage = actor.UserImage;
            userModel.UserPhoneNum = actor.UserPhoneNum;
            userModel.UserEmail = actor.UserEmail;
            userModel.UserDescription = actor.UserDescription;
            userModel.UserAdress = actor.UserAdress;
            userModel.UserRole = Role.USER;
            userModel.UserStatus = Status.AVAILABLE;
            userModel.UserIsDelete = IsDelete.ACTIVE;
            userModel.UserCreateBy = actor.UserCreateBy;
            userModel.UserUpdateTime = actor.UserUpdateTime;
            userModel.UserUpdateBy = actor.UserUpdateBy;

            _context.Users.Add(userModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteActor(int id)
        {
            var actor = await _context.Users.FindAsync(id);
            if (actor == null)
            {
                return false;
            }
            try
            {
                actor.UserIsDelete = IsDelete.ISDELETED;
                _context.Entry(actor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public IQueryable<ActorInfoVM> GetActor(int id)
        {
            var actor = _context.Users
                                    .Where(user => user.UserId == id && user.UserIsDelete == IsDelete.ACTIVE)
                                    .Select(user => new ActorInfoVM
                                    {
                                        UserId = user.UserId,
                                        UserAdress = user.UserAdress,
                                        UserDescription = user.UserDescription,
                                        UserEmail = user.UserEmail,
                                        UserImage = user.UserImage,
                                        UserName = user.UserName,
                                        UserPassword = user.UserPassword,
                                        UserPhoneNum = user.UserPhoneNum,
                                        UserCreateBy = user.UserCreateBy,
                                        UserUpdateBy = user.UserUpdateBy,
                                        UserUpdateTime = user.UserUpdateTime
                                    });
            return actor;
        }

        public async Task<int> UpdateActor(int id, ActorInfoVM actor)
        {
            User user = await _context.Users.FindAsync(id);
            if (user == null) return -1;
            user.UserName = actor.UserName;
            user.UserPassword = actor.UserPassword;
            user.UserPhoneNum = actor.UserPhoneNum;
            user.UserImage = actor.UserImage;
            user.UserEmail = actor.UserEmail;
            user.UserDescription = actor.UserDescription;
            user.UserAdress = actor.UserAdress;
            user.UserUpdateBy = actor.UserUpdateBy;
            user.UserUpdateTime = actor.UserUpdateTime;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return user.UserId;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return -1;
                }
                else throw;
            }
        }
    }

    public interface IUserRepository
    {
        IQueryable<LoginViewModel> CheckLogin(string userEmail, string password);
        IQueryable<ActorBasicInfoVM> GetListActor();
        IQueryable<ActorBasicInfoVM> SearchActor(string userName);
        Task AddActor(ActorInfoVM actor);
        Task<bool> DeleteActor(int id);
        IQueryable<ActorInfoVM> GetActor(int id);
        Task<int> UpdateActor(int id, ActorInfoVM actor);
    }
}
