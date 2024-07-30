namespace CasoPracticoAPI.Entities
{
    public class PuestosTrabajoEnt
    {
        public long idPuesto { get; }
        public int idEmpresa { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public string? requisitos { get; set; }
        public string? fechaPublicacion { get; }
        public string? fechaCierre { get; }

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
