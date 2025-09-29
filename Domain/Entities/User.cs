using System.ComponentModel.DataAnnotations;

namespace TaskMate.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O Nome deve conter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres.")]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
}