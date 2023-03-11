using System.ComponentModel.DataAnnotations;

namespace ExamenSCISA.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo es requerido")]
        public string ApellidoPaterno { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo es requerido")]
        public string ApellidoMaterno { get; set; } = string.Empty;

        public int TipoUsuarioId { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
