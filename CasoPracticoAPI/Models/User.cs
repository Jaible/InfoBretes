namespace InfoBretesAPI.Models
{
    public class User
    {
        public int? IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? Direccion { get; set; }
        public int? IdTipo { get; set; }
        public User() { }
    }
}
