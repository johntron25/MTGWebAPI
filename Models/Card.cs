using System.ComponentModel.DataAnnotations;

namespace MTGCardApi.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Range(0, 20)]
        public int ManaCost { get; set; }

        public string? Rarity { get; set; }
    }
}