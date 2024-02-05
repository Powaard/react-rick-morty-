using System.ComponentModel.DataAnnotations;

namespace YourProject.Models
{
    public class FavoriteCharacter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
