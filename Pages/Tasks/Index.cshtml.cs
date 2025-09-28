using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Pages.Tasks;

public class Index : PageModel
{
    private readonly TaskMateDbContext _context;

    public Index(TaskMateDbContext context) => _context = context;

    public IList<TaskItem> TaskItem { get; set; } = new List<TaskItem>();

    [BindProperty(SupportsGet = true)]
    public string? SearchTitulo { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchStatus { get; set; }

    //public async Task OnGetAsync() => TaskItem = await _context.Tasks.ToListAsync();

    public void OnGet()
    {
        var query = _context.Tasks.AsQueryable();

        if (!string.IsNullOrEmpty(SearchTitulo))
        {
            query = query.Where(t => t.Titulo.Contains(SearchTitulo));
        }

        if (!string.IsNullOrEmpty(SearchStatus))
        {
            query = query.Where(t => t.Status == SearchStatus);
        }

        TaskItem = query.ToList();
    }

    public IActionResult OnPostDelete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
        return RedirectToPage();
    }
}
