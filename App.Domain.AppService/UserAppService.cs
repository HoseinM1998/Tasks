using App.Domain.Core.Contract.User;
using App.Domain.Core.Entites;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.AppService
{
    public class UserAppService : IUseAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserAppService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IdentityResult> Register(User user , CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(user, "123456");
            if (!result.Succeeded)
            {
                return IdentityResult.Failed(result.Errors.ToArray());
            }

            return result;
        }

        public async Task<IdentityResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();
        }


    }
}
