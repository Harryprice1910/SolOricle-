using Microsoft.AspNetCore.Mvc;
using SolOricle.Core.Contracts;
using SolOricle.Core.Models;

namespace SolOricle.Api.Controllers;

[ApiController]
[Route("api/journal")]
public class JournalController : ControllerBase
{
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }

    // POST api/journal
    [HttpPost]
    public async Task<ActionResult<JournalEntry>> Create(
        [FromBody] CreateJournalEntryRequest request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("Title is required.");

        if (string.IsNullOrWhiteSpace(request.Content))
            return BadRequest("Content is required.");

        var entry = await _journalService.CreateAsync(request, ct);
        return Ok(entry);
    }

    // GET api/journal/latest?count=20
    [HttpGet("latest")]
    public async Task<ActionResult<IReadOnlyList<JournalEntry>>> GetLatest(
        [FromQuery] int count = 20,
        CancellationToken ct = default)
    {
        if (count <= 0) return BadRequest("Count must be > 0.");
        if (count > 200) count = 200;

        var entries = await _journalService.GetLatestAsync(count, ct);
        return Ok(entries);
    }
}
