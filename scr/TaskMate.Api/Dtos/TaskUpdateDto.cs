using System.ComponentModel.DataAnnotations;

namespace TaskMate.Api.Dtos
{
    public class TaskUpdateDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string? Title { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}