using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMate.Api.Dtos.Auth;
using TaskMate.Application.Services;
using TaskMate.Infrastructure.Data;

namespace TaskMate.Api.Pages.Auth
{
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
        public LoginDto LoginDto { get; set; } = new();

        public string Message { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == LoginDto.Email);
            if (user == null || !_passwordService.VerifyPassword(LoginDto.Password, user.PasswordHashString))
            {
                Message = "Email ou senha inválidos.";
                return Page();
            }

            Message = "Login bem-sucedido!";

            // TODO: Sign in the user
            return RedirectToPage("/Dashboard/Index");
        }
    }
}