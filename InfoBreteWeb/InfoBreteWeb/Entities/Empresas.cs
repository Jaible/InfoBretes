namespace InfoBreteWeb.Entities
{
    public class Empresas
    {
        public int idEmpresa { get; set; }
        public int idUsuario { get; set; }
        public string? nombreEmpresa { get; set; }
        public string? direccion { get; set; }
        public string? descripcion { get; set; }
        public string? sitioWeb { get; set; }
        public string? telefono { get; set; }
    }
}
