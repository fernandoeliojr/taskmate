using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMate.Infrastructure.Data;
using TaskMate.Domain.Entities;
using TaskMate.Api.Dtos;
using AutoMapper;

namespace TaskMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskMateDbContext _context;
        private readonly IMapper _mapper;

        public TasksController(TaskMateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TaskReadDto>>(tasks));
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return Ok(_mapper.Map<TaskReadDto>(task));
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> CreateTask(TaskCreateDto taskDto)
        {
            var task = _mapper.Map<TaskItem>(taskDto);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var taskReadDto = _mapper.Map<TaskReadDto>(task);

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, taskReadDto);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskUpdateDto taskDto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _mapper.Map(taskDto, task); //atualiza o objeto existente com os dados do DTO

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
