
using InfoBretesWeb.Entities;
using InfoBretesWeb.Services;
using static InfoBretesWeb.Entities.EmpleadosEnt;

namespace InfoBretesWeb.Models
{
    public class EmpleadosModel(HttpClient _http, IConfiguration _configuration) : IEmpleadosModel
    {
        public EmpleadosRespuesta? ConsultarEmpleado(int id)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empleados/ConsultarEmpleado?idUsuario=" + id;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<EmpleadosRespuesta>().Result;

            return null;
        }

        public EmpleadosRespuesta? CrearEmpleado(EmpleadosEnt ent)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empleados/RegistrarEmpleados";

            JsonContent body = JsonContent.Create(ent);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
            {
                return resp.Content.ReadFromJsonAsync<EmpleadosRespuesta>().Result;
            }

            return null;
        }
    }
}
