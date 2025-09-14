using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskMate.Api.Pages.Dashboard
{
    [Authorize]
    public class Index : PageModel
    {
        public void OnGet()
        {
        }
    }
}