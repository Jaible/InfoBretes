namespace InfoBretesAPI.Models
{
    public class PostulacionEnt
    {
        public long idPostulacion { get; set; }
        public int idEmpleado { get; set; }
        public int idPuesto { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string? estadoPostulacion { get; set; }
        public string? linkCurriculum { get;  }
        public string? telefono { get;  }
        public string? fotoPerfil { get;  }
        public string? nombre { get;  }
        public string? email { get; }


    }
    public class PostulacionesRespuesta
    {
        public PostulacionesRespuesta()
        {
            Codigo = "1";
            Mensaje = string.Empty;
            Dato = null;
            Datos = null;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public PostulacionEnt? Dato { get; set; }
        public List<PostulacionEnt>? Datos { get; set; }

    }
}