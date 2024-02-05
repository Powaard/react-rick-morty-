using Microsoft.AspNetCore.Mvc;
using YourProject.Data;
using YourProject.Models;
using System.Linq;

namespace YourProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetFavoriteCharacters()
        {
            var favoriteCharacters = _context.FavoriteCharacters.ToList();
            return Ok(favoriteCharacters);
        }

        [HttpPost]
        public IActionResult AddFavoriteCharacter([FromBody] FavoriteCharacter favoriteCharacter)
        {
            if (_context.FavoriteCharacters.Count() >= 10)
            {
                // Favori karakter ekleme sayısı maksimuma ulaştığında 400 Bad Request döndürüyoruz.
                return BadRequest("Favori karakter ekleme sayısı maksimuma ulaştı (10).");
            }

            _context.FavoriteCharacters.Add(favoriteCharacter);
            _context.SaveChanges();

            // Yeni favori karakter başarıyla eklendiğinde 201 Created döndürüyoruz.
            return CreatedAtAction(nameof(GetFavoriteCharacters), new { id = favoriteCharacter.Id }, favoriteCharacter);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFavoriteCharacter(int id)
        {
            var favoriteCharacter = _context.FavoriteCharacters.Find(id);

            if (favoriteCharacter == null)
            {
                // Belirtilen ID'ye sahip favori karakter bulunamazsa 404 Not Found döndürüyoruz.
                return NotFound();
            }

            _context.FavoriteCharacters.Remove(favoriteCharacter);
            _context.SaveChanges();

            // Favori karakter başarıyla silindiğinde 204 No Content döndürüyoruz.
            return NoContent();
        }
    }
}
