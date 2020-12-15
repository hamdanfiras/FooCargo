using FooCargo.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargo.Models
{
    public static class ModelExtensions
    {
        public static UserProfile ToUserProfile(this ApplicationUser user)
        {
            return new UserProfile
            {
                Email = user.Email,
                FullName = user.FullName
            };
        }
    }
}
