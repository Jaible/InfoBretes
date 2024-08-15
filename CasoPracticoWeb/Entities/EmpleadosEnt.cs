namespace InfoBretesWeb.Entities
{
    public class EmpleadosEnt
    {
        public int idEmpleado {  get; set; }
        public int idUsuario {  get; set; }
        public string linkCurriculum {  get; set; }
        public string fotoPerfil {  get; set; }
        public string telefono {  get; set; }

        public class EmpleadosRespuesta
        {
            public EmpleadosRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public EmpleadosEnt? Dato { get; set; }
            public List<EmpleadosEnt>? Datos { get; set; }
        }
    }
}
