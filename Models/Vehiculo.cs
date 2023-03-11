namespace ExamenSCISA.Models
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Placas { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public int ClienteId { get; set; }

        public string UltimaCita { get; set; } = string.Empty;
        public string ProximaCita { get; set; } = string.Empty;
    }
}
