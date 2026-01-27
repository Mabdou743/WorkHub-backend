using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHub.Application
{
    public interface IIdentityService
    {
        Task<Guid?> CreateUserAsync(string userName, string email, string password);
        Task<bool> ValidateCredentialsAsync(string userName, string password);
        Task<Guid?> GetUserIdByUserNameAsync(string userName);
        Task AddUserToRoleAsync(Guid identityUserId, string role);
        Task DeleteUserAsync(Guid identityUserId);
        Task<bool> UserExistAsync(string username, string email);
        Task<IList<string>> GetUserRolesAsync(Guid identityUserId);
        Task<bool> IsUserActiveAsync(Guid identityUserId);
    }
}
