using System.ComponentModel.DataAnnotations;

namespace ComidaRapida.Models
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100)]
        public string Nombre { get; set;}

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 20)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 20)]
        [Display(Name = "Contraseña")]
        public string Pwd { get; set; }

        public DateTime FechaAlta { get; set; }
        public int RoleId { get; set; }
    }
}
