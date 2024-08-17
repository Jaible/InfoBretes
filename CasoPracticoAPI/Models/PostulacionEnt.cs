namespace InfoBretesAPI.Models
{
    public class Postulacion
    {
        public int idPostulacion { get; set; }
        public int idEmpleado { get; set; }
        public int idPuesto { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string? estadoPostulacion { get; set; }


    }
}
