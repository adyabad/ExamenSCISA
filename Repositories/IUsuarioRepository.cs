using ExamenSCISA.Models;

namespace ExamenSCISA.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<IEnumerable<Usuario>> GetUsuarios();

        public Task<Usuario> GetUsuarioById(int usuarioId);

        public Task CrearUsuario(Usuario usuario);

        public Task EditarUsuario(Usuario usuario);

        public Task EliminarUsuario(int usuarioId);

        public Task<IEnumerable<TipoUsuario>> GetTiposUsuario();
    }
}
