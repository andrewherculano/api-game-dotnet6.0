using System.ComponentModel.DataAnnotations;

namespace GameApi.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo titulo é obrigatorio")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1024, ErrorMessage = "Este campo deve conter no maximo 1024 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo id da categoria é obrigatorio")]
        [Range(1, 1000, ErrorMessage = "Categoria inválida")]
        public int CategoryGameId { get; set; }

        public CategoryGame CategoryGame { get; set; } = null!;
    }
}