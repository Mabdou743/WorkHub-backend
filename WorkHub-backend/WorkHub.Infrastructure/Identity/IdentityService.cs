using Microsoft.AspNetCore.Identity;
using WorkHub.Application;

namespace WorkHub.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Guid?> CreateUserAsync(string userName, string email, string password)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return null;
            }

            return user.Id;
        }

        public async Task<bool> ValidateCredentialsAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
                return false;
            
            var result =  await _signInManager.CheckPasswordSignInAsync(user,password,false);

            return result.Succeeded;

        }

        public async Task<Guid?> GetUserIdByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                user = await _userManager.FindByEmailAsync(userName);
            if (user is null) 
                return null;

            return user?.Id;
        }

        public async Task AddUserToRoleAsync(Guid identityUserId, string role)
        {
            var user = await _userManager.FindByIdAsync(identityUserId.ToString());

            if (user is null)
                throw new ApplicationLayerException("Identity user not found");

            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
                throw new ApplicationLayerException($"Failed to assign role '{role}' to user {identityUserId}");
        }

        public async Task DeleteUserAsync(Guid identityUserId)
        {
            var user = await _userManager.FindByIdAsync(identityUserId.ToString());
            if (user is not null)
                await _userManager.DeleteAsync(user);
        }

        public async Task<bool> UserExistAsync(string username, string email)
        {
            return await _userManager.FindByNameAsync(username) is not null ||
                   await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<IList<string>> GetUserRolesAsync(Guid identityUserId)
        {
            var user = await _userManager.FindByIdAsync(identityUserId.ToString());
            if (user is null)
                throw new ApplicationLayerException("Identity user not found");

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsUserActiveAsync(Guid identityUserId)
        {
            var user =  await _userManager.FindByIdAsync(identityUserId.ToString());
            return user is not null && user.EmailConfirmed;
        }
    }
}
