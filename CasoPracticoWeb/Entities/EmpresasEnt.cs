namespace CasoPracticoWeb.Entities
{
    public class EmpresasEnt
    {
        public long idEmpresa { get; set; }
        public int? idUsuario { get; set; }
        public string? nombreEmpresa { get; set; }
        public string? direccion { get; set; }
        public string? descripcion { get; set; }
        public string? sitioWeb { get; set; }
        public string? telefono { get; set; }

        public class EmpresasRespuesta
        {
            public EmpresasRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public EmpresasEnt? Dato { get; set; }
            public List<EmpresasEnt>? Datos { get; set; }
        }
    }
}
