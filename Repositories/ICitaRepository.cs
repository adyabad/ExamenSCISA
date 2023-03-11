using ExamenSCISA.Models;

namespace ExamenSCISA.Repositories
{
    public interface ICitaRepository
    {
        public Task<IEnumerable<Cita>> GetCitas();
        public Task<IEnumerable<Cita>> GetCitasByIdCliente(int clienteId);

        public Task GetCitaById(int citaId);

        public Task CrearCita(Cita cita);

        public Task EditarCita(Cita cita);

        public Task EliminarCita(int citaId);

        public Task<int> ValidarFechaCita(DateTime fecha);
    }
}
