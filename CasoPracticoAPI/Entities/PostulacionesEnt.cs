namespace CasoPracticoAPI.Entities
{
    public class PostulacionesEnt
    {
        public int idPostulacion { get; }
        public int idEmpleado { get; set; }
        public int idPuesto { get; set; }
        public DateTime fechaPostulacion { get; set; } = DateTime.Now;
        public string? estadoPostulacion { get; set; } = "Pendiente";


        public class PostulacionesRespuesta
        {
            public PostulacionesRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public PostulacionesEnt? Dato { get; set; }
            public List<PostulacionesEnt>? Datos { get; set; }
        }
    }
}
