using App.Domain.Core.Contract.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace App.Endpoint.Razor.Pages.Task
{
    public class ChangeStatusTaskModel : PageModel
    {
        private readonly ITaskAppService _taskAppService;

        [BindProperty]
        public Domain.Core.Entites.Task Task { get; set; }
        public ChangeStatusTaskModel(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }
        public async System.Threading.Tasks.Task OnGetAsync(int taskId, CancellationToken cancellationToken)
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(user))
                {
                    TempData["ErrorMessage"] = "کاربر وارد نشده است.";
                    RedirectToPage("/User/Login");
                    return;
                }

                int userId = int.Parse(user);
                Task = await _taskAppService.GetTaskById(userId, taskId, cancellationToken);

                if (Task == null)
                {
                    TempData["ErrorMessage"] = "تسک پیدا نشد";
                    RedirectToPage("/Task/ListTask");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در بارگذاری تسک: {ex.Message}";
                RedirectToPage("/Task/ListTask");
            }
        }

        public async Task<IActionResult> OnPostAsync(int taskId, bool newStatus, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    TempData["ErrorMessage"] = "کاربر وارد نشده است.";
                    return RedirectToPage("/User/Login");
                }

                int userId = int.Parse(userIdClaim);
                bool result = await _taskAppService.ChangeStatus(userId, taskId, newStatus, cancellationToken);

                if (!result)
                {
                    throw new Exception("خطا در تغییر وضعیت تسک.");
                }

                TempData["Success"] = "وضعیت تسک با موفقیت تغییر کرد.";
                return RedirectToPage("/Task/ListTask");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در تغییر وضعیت تسک: {ex.Message}";
                return RedirectToPage("/Task/ListTask");
            }
        }


    }
}
