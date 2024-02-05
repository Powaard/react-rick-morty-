using YourProject.Models;
using YourProject.Data;
using System.Collections.Generic;
using System.Linq;

namespace YourProject.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IAppDbContext _context;

        public FavoriteService(IAppDbContext context)
        {
            _context = context;
        }

        public List<FavoriteCharacter> GetFavoriteCharacters()
        {
            return _context.FavoriteCharacters.ToList();
        }

        public bool AddFavoriteCharacter(FavoriteCharacter favoriteCharacter)
        {
            if (_context.FavoriteCharacters.Count() >= 10)
            {
                return false;
            }

            _context.FavoriteCharacters.Add(favoriteCharacter);
            _context.SaveChanges();

            return true;
        }

        public bool RemoveFavoriteCharacter(int id)
        {
            var favoriteCharacter = _context.FavoriteCharacters.Find(id);

            if (favoriteCharacter == null)
            {
                return false;
            }

            _context.FavoriteCharacters.Remove(favoriteCharacter);
            _context.SaveChanges();

            return true;
        }
    }
}
