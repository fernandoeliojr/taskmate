using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Pages.Tasks;

public class Edit : PageModel
{
    private readonly TaskMateDbContext _context;
    public Edit(TaskMateDbContext context) => _context = context;

    [BindProperty]
    public TaskItem TaskItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        TaskItem = await _context.Tasks.FindAsync(id);

        if (TaskItem == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var taskFromDb = await _context.Tasks.FindAsync(TaskItem.Id);
        if (taskFromDb == null)
            return NotFound();

        taskFromDb.Titulo = TaskItem.Titulo;
        taskFromDb.Descricao = TaskItem.Descricao;
        taskFromDb.Status = TaskItem.Status;

        await _context.SaveChangesAsync();

        return RedirectToPage("/Tasks/Index");
    }
}