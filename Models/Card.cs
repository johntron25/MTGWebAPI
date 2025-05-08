using System.ComponentModel.DataAnnotations;

namespace MTGCardApi.Models
{
    // this class represents a magic: the gathering card
    public class Card
    {
        // unique identifier for the card
        public int Id { get; set; }

        // name is required and can't be null or empty
        [Required]
        public string Name { get; set; } = string.Empty;

        // type is also required (like creature, instant, etc.)
        [Required]
        public string Type { get; set; } = string.Empty;

        // mana cost must be between 0 and 20
        [Range(0, 20)]
        public int ManaCost { get; set; }

        // rarity is optional (could be common, rare, etc.)
        public string? Rarity { get; set; }
    }
}
