using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMate.Domain.Entities;
using TaskMate.Infrastructure.Data;

namespace TaskMate.Api.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly TaskMateDbContext _context;

        public IndexModel(TaskMateDbContext context)
        {
            _context = context;
        }

        public IList<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public void OnGet()
        {
            Tasks = _context.Tasks.ToList();
        }
    }
}