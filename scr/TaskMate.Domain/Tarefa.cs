using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMate.Domain
{
    public class Tarefa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string Status { get; set; } = "Pendente"; // Pendente, Em Progresso, Concluída
        public string Prioridade { get; set; } = "Média"; // Baixa, Média, Alta
        public DateTime? Prazo { get; set; }

        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; set; }

    }
}