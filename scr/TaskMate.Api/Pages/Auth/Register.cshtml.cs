using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Api.Dtos.Auth;
using TaskMate.Application.Services;
using TaskMate.Domain.Entities;
using TaskMate.Infrastructure.Data;

namespace TaskMate.Api.Pages.Auth
{
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
        public RegisterDto RegisterDto { get; set; } = new();

        public string Message { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.Users.Any(u => u.Email == RegisterDto.Email))
            {
                Message = "Email já cadastrado.";
                return Page();
            }

            var hashedPassword = _passwordService.HashPassword(RegisterDto.Password);

            var user = new User
            {
                Email = RegisterDto.Email,
                PasswordHashString = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            Message = "Usuário registrado com sucesso!";
            return Page();
        }
    }
}