namespace CasoPracticoWeb.Entities
{
    public class PuestosTrabajoEnt
    {
        public long idPuesto { get; set; }
        public int idEmpresa { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public string? requisitos { get; set; }
        public string? ubicacion { get; set; }
        public string? tipoEmpleo { get; set; }
        public string? salario { get; set; }
        public string? nombreEmpresa { get; set; }
        public string? fechaPublicacion { get; set; }
        public string? fechaCierre { get; set; }

        public class PuestosTrabajoRespuesta
        {
            public PuestosTrabajoRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public PuestosTrabajoEnt? Dato { get; set; }
            public List<PuestosTrabajoEnt>? Datos { get; set; }
        }
    }
}
