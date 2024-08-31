namespace InfoBretesAPI.Entities {
    public class PostulacionDTO {
        public int idPostulacion { get; set; }
        public string linkCurriculum { get; set; }
        public string telefono { get; set; }
        public string nombre { get; set; }

        public class PostulacionDTORespuesta {
            public PostulacionDTORespuesta() {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public PostulacionDTO? Dato { get; set; }
            public List<PostulacionDTO>? Datos { get; set; }
        }

    }
}
