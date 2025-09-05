using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskMate.Infrastructure.Data
{
    public class TaskMateDbContextFactory : IDesignTimeDbContextFactory<TaskMateDbContext>
    {
        public TaskMateDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskMateDbContext>();

            // Conexão com SQL Server usando autenticação do Windows
            optionsBuilder.UseSqlServer("Server=.;Database=DB_SistemaTarefas;Trusted_Connection=True;TrustServerCertificate=True;");

            return new TaskMateDbContext(optionsBuilder.Options);
        }
    }
}
