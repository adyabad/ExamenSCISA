namespace ExamenSCISA.Models
{
    public class Cita
    {
		public int CitaId { get; set; }
		public DateTime Fecha { get; set; }

		public DateTime FechaTerminacion { get; set; }
		public string Comentarios { get; set; } = string.Empty;

		public int StatusId { get; set; }

		public string NombreStatus { get; set; }

		public string Cliente { get; set; }

		public int VehiculoId { get; set; }
		public string Vehiculo { get; set; } = string.Empty;
		public int UsuarioId { get; set; }
		public string Usuario { get; set; } = string.Empty;
		public int AdminId { get; set; }
		public string Admin { get; set; } = string.Empty;	
	}
}
