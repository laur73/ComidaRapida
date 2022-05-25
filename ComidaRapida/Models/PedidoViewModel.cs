using System.ComponentModel.DataAnnotations;

namespace ComidaRapida.Models
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }

        [Display(Name = "Platiilo")]
        public string NombrePlatillo { get; set; }

        [Display(Name = "Precio")]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = "El precio debe ser mayor a {1}")]
        public decimal Costo { get; set; }

        [Range(minimum: 1, maximum: 1000, ErrorMessage = "La cantidad debe ser mayor a {1}")]
        public int Cantidad { get; set; }
        public string Estatus { get; set; }
        public string Cliente { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Tipo de Pago")]
        public string TipoPago { get; set; }

        [Display(Name = "Vendedor")]
        public int IdVendedor { get; set; }

        [Display(Name = "Repartidor")]
        public int IdRepartidor { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaFinalizado { get; set; }

        public string Vendedor { get; set; }
        public string Repartidor { get; set; }
    }
}
