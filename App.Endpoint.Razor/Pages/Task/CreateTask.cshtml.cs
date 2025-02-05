using App.Domain.Core.Contract.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace App.Endpoint.Razor.Pages.Task
{
    public class CreateTaskModel : PageModel
    {

        private readonly ITaskAppService _taskAppService;

        [BindProperty]
        public  Domain.Core.Entites.Task Task { get; set; }

        public CreateTaskModel(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    TempData["ErrorMessage"] = "کاربر وارد نشده است";
                    return RedirectToPage("/User/Login");
                }

                int userId = int.Parse(userIdClaim);
                Task.UserId = userId; 
                await _taskAppService.Create(userId, Task, cancellationToken);

                TempData["Success"] = "وظیفه با موفقیت ثبت شد";
                return RedirectToPage("/Task/ListTask");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"خطا در ایجاد تسک: {ex.Message}";
                return Page();
            }
        }

    }
}
