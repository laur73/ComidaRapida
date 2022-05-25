using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComidaRapida.Models
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese un {0}")]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set;}

        [Required(ErrorMessage = "Ingrese un {0}")]
        [StringLength(maximumLength: 20)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Ingrese una {0}")]
        [StringLength(maximumLength: 20)]
        [Display(Name = "Contraseña")]
        public string Pwd { get; set; }

        public DateTime FechaAlta { get; set; }
        public int RoleId { get; set; }
    }
}
