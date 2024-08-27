namespace CasoPracticoWeb.Entities
{
    public class PostulacionEnt
    {
        public long idPostulacion { get; set; }
        public int idEmpleado { get; set; }
        public int idPuesto { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string? estadoPostulacion { get; set; }
        public string? linkCurriculum { get; set; }
        public string? telefono { get; set; }
        public string? fotoPerfil { get; set; }
        public string? nombre { get; set; }
        public string? email { get; set; }



    }
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
        public PostulacionEnt? Dato { get; set; }
        public List<PostulacionEnt>? Datos { get; set; }

    }
}