using ExamenSCISA.Models;
using System.Data.SqlClient;
using ExamenSCISA.Configuration;
using Microsoft.Extensions.Options;

namespace ExamenSCISA.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConfiguracionConexion _connectionString;
        public UsuarioRepository(IOptions<ConfiguracionConexion> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {

            List<Usuario> usuarios = new List<Usuario>();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand("SP_GetUsuarios", cnn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        UsuarioId = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.GetString(3),
                        TipoUsuario = new TipoUsuario()
                        {
                            TipoUsuarioId = reader.GetInt32(4),
                            Tipo = reader.GetString(5)
                        }

                    };

                    usuarios.Add(usuario);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return usuarios;
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "insert into Usuario (Nombre,ApellidoPaterno,ApellidoMaterno, TipoUsuarioId) " +
                    "values (@nombre, @apaterno, @amaterno, @tipoUsuarioId)";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@apaterno", usuario.ApellidoPaterno);
                command.Parameters.AddWithValue("@amaterno", usuario.ApellidoMaterno);
                command.Parameters.AddWithValue("@tipoUsuarioId", usuario.TipoUsuarioId);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public async Task EditarUsuario(Usuario usuario)
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);

            try
            {
                cnn.Open();
                string query = "update Usuario set Nombre = @nombre,ApellidoPaterno = @apaterno, " +
                    "ApellidoMaterno=@amaterno, TipoUsuarioId=@tipoUsuarioId where UsuarioId = @usuarioId";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@usuarioId", usuario.UsuarioId);
                command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@apaterno", usuario.ApellidoPaterno);
                command.Parameters.AddWithValue("@amaterno", usuario.ApellidoMaterno);
                command.Parameters.AddWithValue("@tipoUsuarioId", usuario.TipoUsuarioId);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public async Task EliminarUsuario(int usuarioId)
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "update Usuario set Eliminado = 1 where UsuarioId = @usuarioId";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@usuarioId", usuarioId);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public async Task<IEnumerable<TipoUsuario>> GetTiposUsuario()
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            List<TipoUsuario> tipos = new List<TipoUsuario>();
            try
            {
                cnn.Open();
                string query = "select * from TipoUsuario";
                SqlCommand command = new SqlCommand(query, cnn);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var tipoUsuario = new TipoUsuario()
                    {
                        TipoUsuarioId = reader.GetInt32(0),
                        Tipo = reader.GetString(1)
                    };

                    tipos.Add(tipoUsuario);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return tipos;
        }

        public async Task<Usuario> GetUsuarioById(int usuarioId)
        {
            var usuario = new Usuario();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "select * from Usuario where UsuarioId = @usuarioId";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@usuarioId", usuarioId);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {

                    usuario.UsuarioId = reader.GetInt32(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.ApellidoPaterno = reader.GetString(2);
                    usuario.ApellidoMaterno = reader.GetString(3);
                    usuario.TipoUsuarioId = reader.GetInt32(4);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return usuario;
        }
    }
}
