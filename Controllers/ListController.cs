using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTodo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        private readonly TodoContext _context;

        public ListController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetLists()
        {
            return await _context.TodoLists.Include(list => list.Todos).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetTodoList(int id)
        {
            var list = await _context.TodoLists
                .Include(list => list.Todos)
                .SingleOrDefaultAsync(l => l.Id == id);

            if (list == null)
                return NotFound();

            return list;
        }

        [HttpPost]
        public async Task<ActionResult<TodoList>> PostTodoList(TodoList list)
        {
            _context.TodoLists.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoList), new { id = list.Id }, list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoList(int id, TodoList list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TodoLists.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            var list = await _context.TodoLists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.TodoLists.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
