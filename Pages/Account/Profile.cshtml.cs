using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMate.Data;
using TaskMate.Domain.Entities;
using TaskMate.Services;

namespace TaskMate.Pages.Account;

[Authorize]
public class ProfileModel : PageModel
{
    private readonly TaskMateDbContext _context;
    private readonly PasswordService _passwordService;

    public ProfileModel(TaskMateDbContext context, PasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    [BindProperty]
    public User AppUser { get; set; } = new();

    [BindProperty]
    public ChangePasswordModel ChangePassword { get; set; } = new();

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

    public async Task<IActionResult> OnPostChangePasswordAsync(string newPassword)
    {
        if(!ModelState.IsValid)
            return await OnGetAsync();

        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }

        if(!int.TryParse(userIdClaim, out var userId))
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }

        var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == AppUser.Id);
        if (userDb == null)
            return NotFound();

        //valida senha atual
        if(!_passwordService.VerifyPassword(ChangePassword.CurrentPassword, userDb.PasswordHash, userDb.PasswordSalt))
        {
            ModelState.AddModelError("ChangePassword.CurrentPassword", "Senha atual incorreta.");
            await OnGetAsync();
            return Page();
        }

        //valida confirmação
        if (ChangePassword.NewPassword != ChangePassword.ConfirmPassword)
        {
            ModelState.AddModelError("ChangePassword.ConfirmNewPassword", "A confirmação da nova senha não corresponde.");
            await OnGetAsync();
            return Page();
        }

        //gera hash e salt para a nova senha
        _passwordService.CreatePasswordHash(ChangePassword.NewPassword, out var hash, out var salt);
        userDb.PasswordHash = hash;
        userDb.PasswordSalt = salt;

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Senha alterada com sucesso!";
        return RedirectToPage("/Account/Profile");
    }
}

public class ChangePasswordModel
{
    [Required(ErrorMessage = "Informe a senha atual.")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a nova senha.")]
    [MinLength(6, ErrorMessage = "A nova senha deve ter no mínimo 6 caracteres.")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a nova senha.")]
    [Compare(nameof(NewPassword), ErrorMessage =  "A confirmação da nova senha não corresponde.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}