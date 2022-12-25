using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }
        public int TargetSound { get; set; }
        public int Position { get; set; }
        [Required]
        public string Vocable { get; set; }
    }
}
