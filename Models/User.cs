using System.ComponentModel.DataAnnotations;

namespace GameApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo username é obrigatorio")]
        [MinLength(3, ErrorMessage = "O campo username deve conter no minimo 3 caracteres")]
        [MaxLength(30, ErrorMessage = "O campo username deve conter no maximo 30 caracteres")]
        public string Username { get; set; } = string.Empty;

        [MinLength(6, ErrorMessage = "O campo password deve conter no minimo 6 caracteres")]
        [MaxLength(30, ErrorMessage = "O campo password deve conter no maximo 30 caracteres")]
        [Required(ErrorMessage = "Campo password é obrigatorio")]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}