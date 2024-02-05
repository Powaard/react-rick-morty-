using Microsoft.EntityFrameworkCore;
using YourProject.Models;

namespace YourProject.Data
{
    public interface IAppDbContext
    {
        DbSet<Character> Characters { get; set; }
        DbSet<Episode> Episodes { get; set; }

        DbSet<FavoriteCharacter> FavoriteCharacters { get; set; }
        int SaveChanges();
    }
}
