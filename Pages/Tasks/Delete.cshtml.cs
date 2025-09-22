using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Pages.Tasks;

public class Delete : PageModel
{
    private readonly TaskMateDbContext _context;
    public Delete(TaskMateDbContext context) => _context = context;

    [BindProperty]
    public TaskItem TaskItem { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(int id)
    {
        TaskItem = await _context.Tasks.FindAsync(id);

        if (TaskItem == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(TaskItem.Id);

        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}