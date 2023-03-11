using ExamenSCISA.Configuration;
using ExamenSCISA.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace ExamenSCISA.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ConfiguracionConexion _connectionString;
        public ClienteRepository(IOptions<ConfiguracionConexion> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand("SP_GetClientes", cnn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var cliente = new Cliente()
                    {
                        ClienteId = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.GetString(3),
                        Domicilio = reader.GetString(4),
                        Telefono = reader.GetString(5)

                    };

                    clientes.Add(cliente);
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
            return clientes;
        }
        public async Task CrearCliente(Cliente cliente)
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "insert into Cliente " +
                    "values (@nombre, @apaterno, @amaterno, @telefono, @domicilio, 0)";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@apaterno", cliente.ApellidoPaterno);
                command.Parameters.AddWithValue("@amaterno", cliente.ApellidoMaterno);
                command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@domicilio", cliente.Domicilio);
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

        public async Task EditarCliente(Cliente cliente)
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);

            try
            {
                cnn.Open();
                string query = "update Cliente set Nombre = @nombre,ApellidoPaterno = @apaterno, " +
                    "ApellidoMaterno=@amaterno, Telefono=@telefono, Domicilio=@Domicilio where ClienteId = @clienteId";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@clienteId", cliente.ClienteId);
                command.Parameters.AddWithValue("@nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@apaterno", cliente.ApellidoPaterno);
                command.Parameters.AddWithValue("@amaterno", cliente.ApellidoMaterno);
                command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@domicilio", cliente.Domicilio);
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

        public async Task EliminarCliente(int clienteId)
        {
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "update Cliente set Eliminado = 1 where ClienteId = @clienteId";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@clienteId", clienteId);
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

        public async Task<Cliente> GetClienteById(int clienteId)
        {
            var cliente = new Cliente();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "select * from Cliente where ClienteId = @clienteId";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@clienteId", clienteId);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {

                    cliente.ClienteId = reader.GetInt32(0);
                    cliente.Nombre = reader.GetString(1);
                    cliente.ApellidoPaterno = reader.GetString(2);
                    cliente.ApellidoMaterno = reader.GetString(3);
                    cliente.Telefono = reader.GetString(4);
                    cliente.Domicilio = reader.GetString(5);
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
            return cliente;
        }
        
    }
}
