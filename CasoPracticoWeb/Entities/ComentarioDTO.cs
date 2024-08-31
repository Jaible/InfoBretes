namespace CasoPracticoWeb.Entities {
    public class ComentarioDTO {
        public long idComentario { get; set; }
        public int? idUsuario { get; set; }
        public int? idEmpresa { get; set; }
        public string? comentario { get; set; }
        public int? rating { get; set; }
        public string? Nombre { get; set; }

        public class ComentarioDTORespuesta {
            public ComentarioDTORespuesta() {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public ComentarioDTO? Dato { get; set; }
            public List<ComentarioDTO>? Datos { get; set; }
        }
    }
}
