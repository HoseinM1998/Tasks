using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Endpoint.Razor.Pages.User
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetLogOut()
        {
            HttpContext.Response.Cookies.Delete("ApiKey");

            return RedirectToPage("/User/Login");
        }
    }
}
