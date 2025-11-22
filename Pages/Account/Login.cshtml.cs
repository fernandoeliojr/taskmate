using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Data;
using TaskMate.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace TaskMate.Pages.Account;
public class LoginModel : PageModel
{
    private readonly TaskMateDbContext _context;
    private readonly PasswordService _passwordService;

    public LoginModel(TaskMateDbContext context, PasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    [BindProperty]
    public LoginInputModel Input { get; set; } = new();

    public void OnGet() { }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = _context.Users.FirstOrDefault(u => u.Email == Input.Email);

        if (user == null || !_passwordService.VerifyPassword(Input.Password, user.PasswordHash, user.PasswordSalt))
        {
            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos!");
            return Page();
        }

        //cria identidade
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Nome), //nome do usuário
            new Claim(ClaimTypes.Email, user.Email), //e-mail do usuário
            new Claim(ClaimTypes.Role, user.Role), //role do usuário
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) //ID
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties { IsPersistent = true }
        );

        return RedirectToPage("/Tasks/Index");
    }
}
public class LoginInputModel
{
    [BindProperty]
    [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    public string Password { get; set; } = string.Empty;
}