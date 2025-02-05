using App.Domain.Core.Contract.User;
using App.Domain.Core.Entites.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace App.Endpoint.Razor.Pages.User
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel : PageModel
    {
        private readonly SiteSetting _siteSettings;
        private readonly IUseAppService _userAppService;

        public LoginModel(SiteSetting siteSettings, IUseAppService userAppService)
        {
            _siteSettings = siteSettings;
            _userAppService = userAppService;
        }

        [BindProperty]
        public LoginViewModel PageModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            IdentityResult result = await _userAppService.Login(PageModel.Username, PageModel.Password);

            if (result.Succeeded)
            {
                TempData["Success"] = "وارد شدید";
                return RedirectToPage("/Task/ListTask");
            }
            else
            {
                TempData["Error"] = "نام کاربری یا رمز عبور نادرست است";
                return RedirectToPage("/User/Login");
            }
        }

    }
}