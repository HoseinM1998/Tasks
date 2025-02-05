using App.Domain.AppService;
using App.Domain.Core.Contract.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace App.Endpoint.Razor.Pages.Task
{
    public class DeleteTaskModel : PageModel
    {
        private readonly ITaskAppService _taskAppService;
        public List<Domain.Core.Entites.Task> Tasks { get; set; }
        public DeleteTaskModel(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }
        public async System.Threading.Tasks.Task OnGetAsync(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdClaim);
            Tasks =await _taskAppService.GeTasks(userId, cancellationToken);
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
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

                var result = await _taskAppService.Delete(userId, id, cancellationToken);

                if (result)
                {
                    TempData["Success"] = "تسک با موفقیت حذف شد";
                }
                else
                {
                    TempData["ErrorMessage"] = "تسک مورد نظر پیدا نشد";
                }
                return RedirectToPage("/Task/ListTask");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در حذف تسک: {ex.Message}";
                return RedirectToPage("/Task/ListTask");
            }
        }
    }
}