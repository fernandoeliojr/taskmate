using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Pages.Account;

[Authorize]
public class ProfileModel : PageModel
{
    private readonly TaskMateDbContext _context;

    public ProfileModel(TaskMateDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public User AppUser { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        //verifica se o usuário está autenticado
        if(!User.Identity?.IsAuthenticated ?? true)
            return RedirectToPage("/Account/Login");

        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        //var email = User.Identity?.Name;
        if (string.IsNullOrEmpty(email))
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            await HttpContext.SignOutAsync();
            //return NotFound();
            return RedirectToPage("/Account/Login");
        }

        AppUser = user;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == AppUser.Id);
        if (userDb == null)
            return NotFound();

        userDb.Nome = AppUser.Nome;
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Perfil atualizado com sucesso!";
        return RedirectToPage("/Account/Profile");
    }
}
