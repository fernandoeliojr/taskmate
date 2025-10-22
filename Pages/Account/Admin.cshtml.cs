using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskMate.Pages.Account;

[Authorize(Roles = "Admin")]
public class Admin : PageModel
{
    private readonly ILogger<Admin> _logger;

    public Admin(ILogger<Admin> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {}
}
