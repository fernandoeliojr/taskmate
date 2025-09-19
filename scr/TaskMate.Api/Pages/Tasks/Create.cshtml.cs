using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Domain.Entities;
using TaskMate.Infrastructure.Data;

namespace TaskMate.Api.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly TaskMateDbContext _context;

        public CreateModel(TaskMateDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new();

        public IActionResult OnGet()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tasks.Add(TaskItem);
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}