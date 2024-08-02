namespace CasoPracticoAPI.Entities
{
    public class PostulacionesEnt
    {
        public long idPostulacion { get; }
        public int estadoPostulacion { get; set; }
        public string? linkCurriculum { get; set; }
        public string? telefono { get; set; }
        public string? fotoPerfil { get; set; }
        public string? nombre { get; set; }
        public string? email { get; set; }
   

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
