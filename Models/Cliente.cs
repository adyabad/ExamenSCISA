using System.ComponentModel.DataAnnotations;

namespace ExamenSCISA.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo es requerido")]
        public string ApellidoPaterno { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo es requerido")]
        public string ApellidoMaterno { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo es requerido")]
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo es requerido")]
        public string Domicilio { get; set; } = string.Empty;
    }
}
