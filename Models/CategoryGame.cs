using System.ComponentModel.DataAnnotations;
using GameApi.Models.Enums;

namespace GameApi.Models
{
    public class CategoryGame
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(0, 2, ErrorMessage = "Id da plataforma inválido 0 a 2")]
        public GamePlatform IdPlatform { get; set; }

        public string NamePlatform { get => IdPlatform.ToString(); }
    }
}