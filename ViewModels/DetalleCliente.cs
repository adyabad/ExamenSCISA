using ExamenSCISA.Models;

namespace ExamenSCISA.ViewModels
{
	public class DetalleCliente
	{
		public Cliente Cliente { get; set; }
		public IEnumerable<Vehiculo> Vehiculos { get; set; }
	}
}
