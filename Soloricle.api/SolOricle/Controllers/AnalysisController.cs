using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolOricle.Api.Models;
using SolOricle.Api.Data;

namespace SolOricle.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisController : ControllerBase
    {
        private readonly SolOricleContext _context;

        public AnalysisController(SolOricleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<AnalysisResult>> GetAll()
        {
            return await _context.AnalysisResults.Include(a => a.Literature).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AnalysisResult>> Create(AnalysisResult result)
        {
            _context.AnalysisResults.Add(result);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), result);
        }
    }
}
