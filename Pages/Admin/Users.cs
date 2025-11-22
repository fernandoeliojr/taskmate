using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Pages.Admin;

[Authorize(Roles = "Admin")]

public class UsersModel : PageModel
{
    private readonly TaskMateDbContext _context;

    public UsersModel(TaskMateDbContext context)
    {
        _context = context;
    }

    public IList<User> Users { get; set; } = new List<User>();

    public async Task OnGetAsync()
    {
        Users = await _context.Users
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostToggleRoleAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            TempData["SuccessMessage"] = "Usuário não encontrado.";
            return RedirectToPage();
        }

        user.Role = user.Role == "Admin" ? "User" : "Admin";
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Perfil do usuário '{user.Nome}' foi alterado para '{user.Role}'.";

        return RedirectToPage();
    }
}
