using Microsoft.AspNetCore.Identity;
using Calcio.Web.Data.Entities;
using System.Threading.Tasks;
using Calcio.Web.Models;
using Calcio.Common.Enums;
using System;

namespace Calcio.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserAsync(string email);
        Task<UserEntity> GetUserAsync(Guid userId);
        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path, UserType userType);
        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);


    }
}
