using Microsoft.EntityFrameworkCore;
using TaskMate.Domain.Entities;

namespace TaskMate.Infrastructure.Data
{
    public class TaskMateDbContext : DbContext
    {
        public TaskMateDbContext(DbContextOptions<TaskMateDbContext> options)
            : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}
