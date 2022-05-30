using System.ComponentModel.DataAnnotations;

namespace ComidaRapida.Models
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "Ingrese un {0}")]
        [StringLength(maximumLength: 50)]
        [Display(Name = "Platillo")]
        public string NombrePlatillo { get; set; }

        [Required(ErrorMessage = "Ingrese un Precio")]
        [Display(Name = "Precio")]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = "El precio debe ser mayor a {1}")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "Ingrese una {0}")]
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "La cantidad debe ser mayor a {1}")]
        public int Cantidad { get; set; }
        public string Estatus { get; set; }

        [Required(ErrorMessage = "Ingrese un {0}")]
        [StringLength(maximumLength: 50)]
        public string Cliente { get; set; }

        [Required(ErrorMessage = "Ingrese una {0}")]
        [StringLength(maximumLength: 100)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese un {0}")]
        [Display(Name = "Tipo de Pago")]
        public string TipoPago { get; set; }

        public int IdVendedor { get; set; }

        public int IdRepartidor { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaFinalizado { get; set; } = DateTime.Now;

        public string Vendedor { get; set; }
        public string Repartidor { get; set; }
    }
}
