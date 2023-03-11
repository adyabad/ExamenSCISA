using ExamenSCISA.Configuration;
using ExamenSCISA.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace ExamenSCISA.Repositories
{
	public class VehiculoRepository : IVehiculoRepository
	{
        private readonly ConfiguracionConexion _connectionString;
        public VehiculoRepository(IOptions<ConfiguracionConexion> connectionString)
        {
            _connectionString = connectionString.Value;
        }
        public async Task CrearVehiculo(Vehiculo vehiculo)
		{
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "insert into Vehiculo " +
                    "values (@marca, @modelo, @placas, @color, 0, @clienteId)";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@marca", vehiculo.Marca);
                command.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                command.Parameters.AddWithValue("@placas", vehiculo.Placas);
                command.Parameters.AddWithValue("@color", vehiculo.Color);
                command.Parameters.AddWithValue("@clienteId", vehiculo.ClienteId);
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

		public async Task EliminarVehiculo(int vehiculoId)
		{
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();                
                SqlCommand command = new SqlCommand("SP_EliminarVehiculo", cnn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@vehiculoId", vehiculoId);
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

		public async Task<IEnumerable<Vehiculo>> GetVehiculosByIdCliente(int clienteId)
		{
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand("SP_GetVehiculosByCliente", cnn);
                command.Parameters.AddWithValue("@clienteId", clienteId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var vehiculo = new Vehiculo()
                    {
                        VehiculoId = reader.GetInt32(0),
                        Marca = reader.GetString(1),
                        Modelo = reader.GetString(2),
                        Placas = reader.GetString(3),
                        Color = reader.GetString(4),
                        UltimaCita = reader.GetString(5),
                        ProximaCita = reader.GetString(6)

                    };

                    vehiculos.Add(vehiculo);
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
            return vehiculos;
        }
	}
}
