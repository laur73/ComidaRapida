using Microsoft.Data.SqlClient;
using ComidaRapida.Models;
using Dapper;

namespace ComidaRapida.Repositorios
{
    public interface IRepositorioPedidos
    {
        Task Crear(PedidoViewModel pedido);
        Task<IEnumerable<PedidoViewModel>> Listar();
    }

    public class RepositorioPedidos : IRepositorioPedidos
    {

        private readonly string connectionString;

        public RepositorioPedidos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<PedidoViewModel>> Listar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<PedidoViewModel>(
                @"SELECT IdPedido, NombrePlatillo, Costo, Cantidad, Estatus, Cliente, Direccion, TipoPago,
                v.Nombre AS Vendedor, r.Nombre AS Repartidor, p.FechaAlta, FechaFinalizado
                FROM Pedidos p
                INNER JOIN Usuarios v
                ON v.IdUsuario = Vendedor
                INNER JOIN Usuarios r
                ON r.IdUsuario = Repartidor");
        }

        public async Task Crear(PedidoViewModel pedido)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Pedidos (NombrePlatillo, Costo, Cantidad, Estatus, Cliente, Dirección, TipoPago, Vendedor, FechaAlta)
                VALUES (@NombrePlatillo, @Costo, @Cantidad, @Estatus, @ Cliente, @Direccion, @TipoPago, @Vendedor, GETDATE());    
                SELECT SCOPE_IDENTITY();", pedido);
            pedido.IdPedido = id;
        }
    }
}
