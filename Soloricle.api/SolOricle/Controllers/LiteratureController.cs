using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolOricle.Api.Models;
using SolOricle.Api.Data;




namespace SolOricle.Api.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class LiteratureController : ControllerBase
    {
        private readonly SolOricleContext _context;

        public LiteratureController(SolOricleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Literature>> GetAll()
        {
            return await _context.Literature.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Literature>> GetById(int id)
        {
            var lit = await _context.Literature.FindAsync(id);
            if (lit == null) return NotFound();
            return lit;
        }

        [HttpPost]
        public async Task<ActionResult<Literature>> Create(Literature lit)
        {
            _context.Literature.Add(lit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = lit.Id }, lit);
        }
    }
}
