using System.ComponentModel.DataAnnotations;

namespace TaskMate.Api.Dtos
{
    public class TaskCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Description { get; set; }
        
    }
}