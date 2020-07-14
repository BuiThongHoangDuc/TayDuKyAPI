using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKyAPI.ViewModel
{
    public class ActorInfoVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserDescription { get; set; }
        public string UserPhoneNum { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserAdress { get; set; }
        public string UserCreateBy { get; set; }
        public DateTime? UserUpdateTime { get; set; }
        public string UserUpdateBy { get; set; }
    }
}
