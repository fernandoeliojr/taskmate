using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMate.Domain
{
    public class Categoria
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }

        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
    }
}