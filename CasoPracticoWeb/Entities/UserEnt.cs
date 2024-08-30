namespace InfoBretesWeb.Entities
{
    public class UserEnt
    {
        public int? IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? Direccion { get; set; }
        public int? IdTipo { get; set; }
        public UserEnt() { }

        public class UserRespuesta
        {
            public UserRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public UserEnt? Dato { get; set; }
            public List<UserEnt>? Datos { get; set; }
        }
    }
}
