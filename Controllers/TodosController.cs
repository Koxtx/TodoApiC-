    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

[ApiController]
    [Route("api/todos")]
    public class TodosController: ControllerBase
    {
    private readonly TodoDbContext _context;
    public TodosController(TodoDbContext context)
    {
        _context=context;
    
    }

        [HttpGet]
    public async Task<IActionResult> GetTodos()
        {
      var  todos = await _context.Todos.ToListAsync();

       return Ok(todos);
            
        }

    [HttpPost]
public async Task<IActionResult> CreateTodo(CreateTodoDto dto)
{
    var todo = new Todo
    {
        Title=dto.Title,
        Description=dto.Description,
        IsCompleted=false,
        CreatedAt= DateTime.UtcNow
    };
    _context.Todos.Add(todo);
    await _context.SaveChangesAsync();
     return CreatedAtAction(
    nameof(GetTodoById),
    new { id = todo.Id },
    todo
);
    }

[HttpGet("{id}")]
public async Task<IActionResult> GetTodoById(int id){
var todo = await _context.Todos.FindAsync(id);
if ( todo==null){
    return NotFound();
}

    return Ok(todo);


}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTodo (int id){
    var todo = await _context.Todos.FindAsync(id);
if ( todo==null){
    return NotFound();
}

_context.Todos.Remove(todo);
await _context.SaveChangesAsync();
return NoContent();

}


[HttpPut("{id}")]
public async Task<IActionResult> UpdateTodo(int id, UpdateTodoDto dto)
{
 var todo = await _context.Todos.FindAsync(id);
 if ( todo==null){
    return NotFound();
}
    todo.Title= dto.Title;
    todo.Description=dto.Description;

await _context.SaveChangesAsync();
return NoContent();
}


[HttpPatch("{id}/complete")]
public async Task<IActionResult> CompleteTodo(int id)
{
 var todo = await _context.Todos.FindAsync(id);
 if ( todo==null){
    return NotFound();
}
todo.IsCompleted =true;
await _context.SaveChangesAsync();
return NoContent();
}

    }