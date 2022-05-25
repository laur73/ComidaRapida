using Microsoft.Data.SqlClient;
using ComidaRapida.Models;
using Dapper;

namespace ComidaRapida.Repositorios
{
    public interface IRepositorioPedidos
    {
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
                @"SELECT IdPedido, NombrePlatillo, Costo, Cantidad, Cliente, Direccion, TipoPago, v.Nombre AS Vendedor, r.Nombre AS Repartidor, p.FechaAlta, FechaFinalizado, Estatus
                FROM Pedidos p
                INNER JOIN Usuarios v
                ON v.IdUsuario = Vendedor
                INNER JOIN Usuarios r
                ON r.IdUsuario = Repartidor");
        }

        public async Task Crear(PedidoViewModel pedido)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Pedidos (NombrePlatillo, Costo, Cantidad, Cliente, Dirección, TipoPago, Vendedor, FechaAlta, Estatus)
                                                        VALUES (@Nombre, @Usuario, @Pwd, GETDATE(),3);    
                                                        SELECT SCOPE_IDENTITY();", pedido);
            pedido.IdPedido = id;
        }
    }
}
