using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all todo items.
        /// </summary>
        /// <returns>List of todo items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific todo item by ID.
        /// </summary>
        /// <param name="id">The ID of the todo item.</param>
        /// <returns>The todo item with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoById(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();
            return todo;
        }

        /// <summary>
        /// Creates a new todo item.
        /// </summary>
        /// <param name="todo">The todo item to create.</param>
        /// <returns>The created todo item.</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        /// <summary>
        /// Updates an existing todo item.
        /// </summary>
        /// <param name="id">The ID of the todo item to update.</param>
        /// <param name="updatedTodo">The updated todo item details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoItem updatedTodo)
        {
            if (id != updatedTodo.Id) return BadRequest();

            _context.Entry(updatedTodo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific todo item.
        /// </summary>
        /// <param name="id">The ID of the todo item to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
