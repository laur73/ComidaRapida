using Microsoft.Data.SqlClient;
using ComidaRapida.Models;
using Dapper;

namespace ComidaRapida.Repositorios
{
    public interface IRepositorioVendedores
    {
        Task Actualizar(UsuarioViewModel vendedor);
        Task Crear(UsuarioViewModel vendedor);
        Task Eliminar(int IdVendedor);
        Task<bool> Existe(string nombre, string usuario);
        Task<IEnumerable<UsuarioViewModel>> Listar();
        Task<UsuarioViewModel> ObtenerId(int IdVendedor);
    }
    public class RepositorioVendedores : IRepositorioVendedores
    {
        private readonly string connectionString;

        public RepositorioVendedores(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<UsuarioViewModel>(@"SELECT IdUsuario, Nombre, Usuario, FechaAlta FROM Usuarios WHERE roleId = 2");
        }


        public async Task<bool> Existe(string nombre, string usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 FROM Usuarios WHERE Nombre = @Nombre AND Usuario = @Usuario", new { nombre, usuario });
            return existe == 1;
        }

        public async Task Crear(UsuarioViewModel vendedor)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Usuarios (Nombre, Usuario, Pwd, FechaAlta, RoleId)
                                                        VALUES (@Nombre, @Usuario, @Pwd, GETDATE(),2);    
                                                        SELECT SCOPE_IDENTITY();", vendedor);
            vendedor.IdUsuario = id;
        }

        public async Task<UsuarioViewModel> ObtenerId(int IdVendedor)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<UsuarioViewModel>(
                                    @"SELECT * FROM Usuarios WHERE IdUsuario = @IdVendedor", new { IdVendedor });
        }

        public async Task Actualizar(UsuarioViewModel vendedor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Usuarios SET Nombre = @Nombre, Usuario = @Usuario WHERE IdUsuario = @IdUsuario", vendedor);
        }

        public async Task Eliminar(int IdVendedor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Usuarios WHERE IdUsuario = @IdVendedor", new { IdVendedor });
        }
    }
}
