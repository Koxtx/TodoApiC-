    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

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

    
    }
