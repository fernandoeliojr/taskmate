using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskMate.Api.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInputModel Input { get; set; } = new();

        public class LoginInputModel
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.Email == "admin@taskmate.com" && Input.Password == "Admin123!")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Input.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true, //mantém login ativo
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                    });

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
            return Page();
        }
    }
}