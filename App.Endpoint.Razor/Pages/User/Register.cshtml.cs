using App.Domain.Core.Contract.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Endpoint.Razor.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly IUseAppService _userAppService;

        public CreateModel(IUseAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [BindProperty]
        public Domain.Core.Entites.User User { get; set; }


        public async System.Threading.Tasks.Task OnGet(CancellationToken cancellationToken)
        {
            
        }


        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
           await _userAppService.Register(User, cancellationToken);
            return RedirectToPage("Login");
        }
    }
}
