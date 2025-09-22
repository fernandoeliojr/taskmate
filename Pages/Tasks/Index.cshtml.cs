using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Pages.Tasks;

public class Index : PageModel
{
    private readonly TaskMateDbContext _context;

    public Index(TaskMateDbContext context) => _context = context;

    public IList<TaskItem> TaskItem { get; set; } = new List<TaskItem>();
    public async Task OnGetAsync() => TaskItem = await _context.Tasks.ToListAsync();
}
