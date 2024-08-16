namespace InfoBretesAPI.Models
{
    public class SolicitudModel
    {
        public int idSolicitud { get; set; }
        public int idUsuario { get; set; }
        public int idPostulacion { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public IFormFile Curriculum { get; set; }
        public IFormFile Otrosdocumentos { get; set; }
        public string estadoPostulacion { get; set; }


    }
}
