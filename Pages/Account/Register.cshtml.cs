using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Data;
using TaskMate.Domain.Entities;
using TaskMate.Services;

namespace TaskMate.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly TaskMateDbContext _context;
    private readonly PasswordService _passwordService;

    public RegisterModel(TaskMateDbContext context, PasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    [BindProperty]
    public RegisterInputModel Input { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        if (_context.Users.Any(u => u.Email == Input.Email))
        {
            ModelState.AddModelError("Input.Email", "E-mail já está em uso.");
            return Page();
        }

        _passwordService.CreatePasswordHash(Input.Password, out var hash, out var salt);

        var user = new User
        {
            Nome = Input.Nome,
            Email = Input.Email,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        TempData["Success"] = "Usuário cadastrado com sucesso! Faça login.";
        return RedirectToPage("/Account/Login");
    }
}

public class RegisterInputModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
    public string Password { get; set; } = string.Empty;
}
