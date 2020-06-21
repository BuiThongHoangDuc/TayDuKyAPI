using System;
using System.Collections.Generic;

namespace TayDuKyAPI.Models
{
    public partial class User
    {
        public User()
        {
            ActorRoles = new HashSet<ActorRole>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserDescription { get; set; }
        public string UserPhoneNum { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserAdress { get; set; }
        public int UserRole { get; set; }
        public int UserStatus { get; set; }

        public virtual ICollection<ActorRole> ActorRoles { get; set; }
    }
}
