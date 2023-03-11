using ExamenSCISA.Models;

namespace ExamenSCISA.Repositories
{
    public interface IVehiculoRepository
    {
        public Task<IEnumerable<Vehiculo>> GetVehiculosByIdCliente(int clienteId);

        public Task CrearVehiculo(Vehiculo vehiculo);

        public Task EliminarVehiculo(int vehiculoId);
    }
}
