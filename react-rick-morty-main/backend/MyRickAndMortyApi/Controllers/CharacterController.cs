[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly IAppDbContext _context;

    public CharacterController(IAppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        var characters = await _context.Characters.ToListAsync();
        return Ok(characters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Character>> GetCharacter(int id)
    {
        var character = await _context.Characters.FindAsync(id);

        if (character == null)
        {
            return NotFound();
        }

        return Ok(character);
    }


    [HttpPost]
    public async Task<ActionResult<Character>> AddCharacter(Character character)
    {
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCharacter(int id)
    {
        var character = await _context.Characters.FindAsync(id);

        if (character == null)
        {
            return NotFound();
        }

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
