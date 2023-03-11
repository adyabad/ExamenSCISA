using ExamenSCISA.Models;

namespace ExamenSCISA.Repositories
{
    public interface IClienteRepository
    {
        public Task<IEnumerable<Cliente>> GetClientes();

        public Task<Cliente> GetClienteById(int clienteId);

        public Task CrearCliente(Cliente cliente);

        public Task EditarCliente(Cliente cliente);

        public Task EliminarCliente(int clienteId);
    }
}
