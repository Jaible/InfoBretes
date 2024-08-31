namespace CasoPracticoAPI.Entities
{
    public class ComentarioEnt
    {
        public long idComentario {  get; set; }
        public int? idUsuario { get; set; }
        public int? idEmpresa { get; set; }
        public string? comentario { get; set; }
        public int? rating { get; set; }

        public class ComentarioRespuesta
        {
            public ComentarioRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public ComentarioEnt? Dato { get; set; }
            public List<ComentarioEnt>? Datos { get; set; }
        }
    }
}
