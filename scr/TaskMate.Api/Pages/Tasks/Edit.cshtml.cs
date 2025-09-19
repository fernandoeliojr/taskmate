using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Domain.Entities;
using TaskMate.Infrastructure.Data;

namespace TaskMate.Api.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly TaskMateDbContext _context;

        public EditModel(TaskMateDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new();

        public IActionResult OnGet(Guid? id)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tasks.Update(TaskItem);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}