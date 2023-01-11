using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Get /api/user
  
    public class UsersController : ControllerBase 
    {
        private readonly DataContext _context;
      public UsersController(DataContext context)
      {
            _context = context;
        
      }
      [HttpGet]
      public async Task< ActionResult<IEnumerable<AppUser>>> GetUser() //controllerBase is declared to get actionResult
      {
        var users=await _context .Users .ToListAsync ();
        return users;
      }
      [HttpGet("{id}")]
      public async Task <ActionResult<AppUser>> GetUser(int id)
      {
        return await _context .Users .FindAsync(id);
      }

    }
}