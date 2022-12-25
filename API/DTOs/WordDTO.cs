using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class WordDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A vocable is required")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100!")]
        public string Vocable { get; set; }
    }
}
