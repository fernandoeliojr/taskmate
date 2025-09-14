using Microsoft.AspNetCore.Mvc;
using TaskMate.Api.Dtos.Auth;
using TaskMate.Infrastructure.Data;
using TaskMate.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using TaskMate.Domain.Entities;
using System.Security.Cryptography;
using TaskMate.Application.Services;

namespace TaskMate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TaskMateDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly PasswordService _passwordService;

        public AuthController(TaskMateDbContext context, ITokenService tokenService, PasswordService passwordService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email já está em uso.");
            }

            // Cria o hash da senha
            var hashedPassword = _passwordService.HashPassword(registerDto.Password);

            var user = new User
            {
                Email = registerDto.Email,
                PasswordHashString = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário registrado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            var isPasswordValid = _passwordService.VerifyPassword(loginDto.Password, user.PasswordHashString);
            if (!isPasswordValid)
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}