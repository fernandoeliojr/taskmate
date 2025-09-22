using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Pages.Tasks;

public class Create : PageModel
{
    private readonly TaskMateDbContext _context;
    public Create(TaskMateDbContext context) => _context = context;
    
    [BindProperty]
    public TaskItem TaskItem { get; set; } = new TaskItem();
    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.Tasks.Add(TaskItem);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}