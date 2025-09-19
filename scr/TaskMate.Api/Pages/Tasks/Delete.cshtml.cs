using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Domain.Entities;
using TaskMate.Infrastructure.Data;

namespace TaskMate.Api.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly TaskMateDbContext _context;

        public DeleteModel(TaskMateDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new();

        public IActionResult OnGet(Guid id)
        {
            TaskItem = _context.Tasks.Find(id);
            if (TaskItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var task = _context.Tasks.Find(TaskItem.Id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}