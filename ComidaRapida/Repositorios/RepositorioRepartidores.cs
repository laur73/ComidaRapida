using Microsoft.Data.SqlClient;
using ComidaRapida.Models;
using Dapper;

namespace ComidaRapida.Repositorios
{
    public interface IRepositorioRepartidores
    {
        Task Actualizar(UsuarioViewModel repartidor);
        Task Crear(UsuarioViewModel repartidor);
        Task Eliminar(int IdRepartidor);
        Task<bool> Existe(string nombre, string usuario);
        Task<IEnumerable<UsuarioViewModel>> Listar();
        Task<UsuarioViewModel> ObtenerId(int IdRepartidor);
    }

    public class RepositorioRepartidores : IRepositorioRepartidores
    {
        private readonly string connectionString;

        public RepositorioRepartidores(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<UsuarioViewModel>(@"SELECT IdUsuario, Nombre, Usuario, FechaAlta FROM Usuarios WHERE roleId = 3");
        }

        public async Task<bool> Existe(string nombre, string usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 FROM Usuarios WHERE Nombre = @Nombre AND Usuario = @Usuario", new { nombre, usuario });
            return existe == 1;
        }

        public async Task Crear(UsuarioViewModel repartidor)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Usuarios (Nombre, Usuario, Pwd, FechaAlta, RoleId)
                                                        VALUES (@Nombre, @Usuario, @Pwd, GETDATE(),3);    
                                                        SELECT SCOPE_IDENTITY();", repartidor);
            repartidor.IdUsuario = id;
        }

        public async Task<UsuarioViewModel> ObtenerId(int IdRepartidor)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<UsuarioViewModel>(
                                    @"SELECT * FROM Usuarios WHERE IdUsuario = @IdRepartidor", new { IdRepartidor });
        }

        public async Task Actualizar(UsuarioViewModel repartidor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Usuarios SET Nombre = @Nombre, Usuario = @Usuario WHERE IdUsuario = @IdUsuario", repartidor);
        }

        public async Task Eliminar(int IdRepartidor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Usuarios WHERE IdUsuario = @IdRepartidor", new { IdRepartidor });
        }
    }
}
