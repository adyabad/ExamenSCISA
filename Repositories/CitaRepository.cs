using ExamenSCISA.Configuration;
using ExamenSCISA.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace ExamenSCISA.Repositories
{
	public class CitaRepository : ICitaRepository
	{
        private readonly ConfiguracionConexion _connectionString;
        public CitaRepository(IOptions<ConfiguracionConexion> connectionString)
        {
            _connectionString = connectionString.Value;
        }

		public async Task CrearCita(Cita cita)
		{
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {
                cnn.Open();
                string query = "insert into Cita " +
                    "values (@fechaCita, @comentarios, @fechaTerminacion, @vehiculoId, 1, null, null)";
                SqlCommand command = new SqlCommand(query, cnn);
                command.Parameters.AddWithValue("@fechaCita", cita.Fecha);
                command.Parameters.AddWithValue("@comentarios", cita.Comentarios);
                command.Parameters.AddWithValue("@fechaTerminacion", cita.FechaTerminacion);
                command.Parameters.AddWithValue("@vehiculoId", cita.VehiculoId);
                //command.Parameters.AddWithValue("@adminId", cita.AdminId);
                //command.Parameters.AddWithValue("@usuarioId", cita.UsuarioId);
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

		public Task EditarCita(Cita cita)
		{
			throw new NotImplementedException();
		}

		public Task EliminarCita(int citaId)
		{
			throw new NotImplementedException();
		}

		public Task GetCitaById(int citaId)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Cita>> GetCitas()
		{
            var citas = new List<Cita>();
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand("SP_GetCitas", cnn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var cita = new Cita()
                    {
                        CitaId = reader.GetInt32(0),
                        Fecha = reader.GetDateTime(1),
                        Comentarios = reader.GetString(2),
                        FechaTerminacion = reader.GetDateTime(3),
                        NombreStatus = reader.GetString(4),
                        Cliente = reader.GetString(5),
                        Vehiculo = reader.GetString(6)
                        
                    };

                    citas.Add(cita);
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
            return citas;
        }

		public Task<IEnumerable<Cita>> GetCitasByIdCliente(int clienteId)
		{
			throw new NotImplementedException();
		}

        public async Task<int> ValidarFechaCita(DateTime fecha)
        {
            int count = 0;
            var cnn = new SqlConnection(_connectionString.ConnectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand("SP_ValidarFechaCita", cnn);
                command.Parameters.AddWithValue("@fechaCita", fecha);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    count = reader.GetInt32(0);                      
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
            return count;
        }
    }
}
