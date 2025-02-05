using App.Domain.Core.Contract.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace App.Endpoint.Razor.Pages.Task
{
    public class ListTaskModel : PageModel
    {
        private readonly ITaskAppService _taskAppService;

        [BindProperty]
        public List<Domain.Core.Entites.Task> Tasks { get; set; }
        public int TaskUnfinished { get; set; }

        public ListTaskModel(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
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
               Tasks= await _taskAppService.GeTasks(userId, cancellationToken);
               TaskUnfinished = await _taskAppService.GetIsCompleted(userId, cancellationToken);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"خطا در نمایش لیست: {ex.Message}";
                return RedirectToPage("/Index");
            }
        }
    }
}
