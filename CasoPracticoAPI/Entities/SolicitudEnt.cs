
    namespace CasoPracticoAPI.Entities
    {
        public class SolicitudEnt
        {
            public int idSolicitud { get; set; }
            public int idUsuario { get; set; }
            public int idPostulacion { get; set; }
            public DateTime fechaSolicitud { get; set; }
            public string estadoPostulacion { get; set; }
            public byte[] Curriculum { get; set; }
            public byte[] OtrosDocumentos { get; set; }
        }
    }