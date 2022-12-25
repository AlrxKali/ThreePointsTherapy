using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddWordDTO
    {
        //Esta validación es importante sino se crear vacía el nombre de categoría
        [Required(ErrorMessage = "A vocable is required")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100!")]
        public string Nombre { get; set; }
    }
}
