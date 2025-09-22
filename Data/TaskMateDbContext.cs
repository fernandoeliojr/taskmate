using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskMate.Domain.Entities;

namespace TaskMate.Data;

public class TaskMateDbContext : DbContext
{
    public TaskMateDbContext(DbContextOptions<TaskMateDbContext> options)
        : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
}
