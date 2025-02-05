using App.Domain.Core.Contract.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace App.Endpoint.Razor.Pages.Task
{
    public class UpdateTaskModel : PageModel
    {
        private readonly ITaskAppService _taskAppService;

        [BindProperty]
        public Domain.Core.Entites.Task Task { get; set; }

        public UpdateTaskModel(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public async Task<IActionResult> OnGetAsync(int id,CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                TempData["ErrorMessage"] = "کاربر وارد نشده است";
                return RedirectToPage("/User/Login");
            }

            int userId = int.Parse(userIdClaim);
            var task = await _taskAppService.GetTaskById(userId, id, cancellationToken);

            if (task == null)
            {
                TempData["ErrorMessage"] = "تسک یافت نشد یا شما مجاز به ویرایش آن نیستید";
                return RedirectToPage("/Task/ListTask");
            }
            Task = task;
            return Page();
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
                var result = await _taskAppService.Update(userId, Task.Id, Task, cancellationToken);

                if (result)
                {
                    TempData["Success"] = "تسک با موفقیت ویرایش شد";
                    return RedirectToPage("/Task/ListTask");
                }
                else
                {
                    TempData["ErrorMessage"] = "خطا در ویرایش تسک";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا: {ex.Message}";
                return Page();
            }
        }
    }
}
