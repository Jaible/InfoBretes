// Models / ArchivoModel.cs
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;


namespace CasoPracticoWeb.Models
{
    public class Archivo
    {
        [Key]
        public int IdArchivo { get; set; }

        public int IdSolicitud { get; set; }

        [Required]
        [StringLength(256)]
        public string NombreArchivo { get; set; }

        [Required]
        public byte[] DatosArchivo { get; set; }

        public Solicitud Solicitud { get; set; }
    }
}