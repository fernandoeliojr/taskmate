using System.ComponentModel.DataAnnotations;

namespace TaskMate.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    public string Status { get; set; } = "A fazer";
}
