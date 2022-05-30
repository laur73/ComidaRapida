using Microsoft.Data.SqlClient;
using ComidaRapida.Models;
using Dapper;

namespace ComidaRapida.Repositorios
{
    public interface IRepositorioPedidos
    {
        Task Actualizar(PedidoViewModel pedido);
        Task Crear(PedidoViewModel pedido);
        Task Eliminar(int IdPedido);
        Task Entregar(PedidoViewModel pedido);
        Task<IEnumerable<PedidoViewModel>> Listar();
        Task<PedidoViewModel> ObtenerId(int IdPedido);
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
                LEFT JOIN Usuarios r
                ON r.IdUsuario = Repartidor");
        }

        public async Task Crear(PedidoViewModel pedido)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Pedidos (NombrePlatillo, Costo, Cantidad, Estatus, Cliente, Direccion, TipoPago, Vendedor, FechaAlta, FechaFinalizado)
                VALUES (@NombrePlatillo, @Costo, @Cantidad, @Estatus, @Cliente, @Direccion, @TipoPago, 5, GETDATE(),'');    
                SELECT SCOPE_IDENTITY();", pedido);
            pedido.IdPedido = id;
        }

        public async Task<PedidoViewModel> ObtenerId(int IdPedido)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<PedidoViewModel>(
                                    @"SELECT * FROM Pedidos WHERE IdPedido = @IdPedido", new { IdPedido });
        }

        public async Task Actualizar(PedidoViewModel pedido)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                @"UPDATE Pedidos SET NombrePlatillo = @NombrePlatillo, Costo = @Costo, Cantidad = @Cantidad, Cliente = @Cliente,
                Direccion = @Direccion, TipoPago = @TipoPago
                WHERE IdPedido = @IdPedido", pedido);
        }

        public async Task Eliminar(int IdPedido)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Pedidos WHERE IdPedido = @IdPedido", new { IdPedido });
        }

        public async Task Entregar(PedidoViewModel pedido)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                @"UPDATE Pedidos SET Estatus = 'Entregado', Repartidor = 3, FechaFinalizado = GETDATE()
                WHERE IdPedido = @IdPedido", pedido);
        }


    }
}
