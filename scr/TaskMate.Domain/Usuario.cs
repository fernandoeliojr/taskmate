using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMate.Domain
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}