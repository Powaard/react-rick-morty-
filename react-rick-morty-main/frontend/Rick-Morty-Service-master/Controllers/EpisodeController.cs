[ApiController]
[Route("api/[controller]")]
public class EpisodeController : ControllerBase
{
    private readonly AppDbContext _context;

    public EpisodeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Episode>>> GetEpisodes()
    {
        var episodes = await _context.Episodes.ToListAsync();
        return Ok(episodes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Episode>> GetEpisode(int id)
    {
        var episode = await _context.Episodes.FindAsync(id);

        if (episode == null)
        {
            return NotFound("Episode not found");
        }

        return Ok(episode);
    }

    [HttpPost]
    public async Task<ActionResult<Episode>> CreateEpisode(Episode episode)
    {
        _context.Episodes.Add(episode);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEpisode), new { id = episode.Id }, episode);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEpisode(int id, Episode updatedEpisode)
    {
        if (id != updatedEpisode.Id)
        {
            return BadRequest("Invalid episode ID");
        }

        _context.Entry(updatedEpisode).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EpisodeExists(id))
            {
                return NotFound("Episode not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEpisode(int id)
    {
        var episode = await _context.Episodes.FindAsync(id);
        if (episode == null)
        {
            return NotFound("Episode not found");
        }

        _context.Episodes.Remove(episode);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EpisodeExists(int id)
    {
        return _context.Episodes.Any(e => e.Id == id);
    }
}
