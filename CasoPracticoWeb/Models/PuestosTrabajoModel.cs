using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using static CasoPracticoWeb.Entities.PuestosTrabajoEnt;


namespace CasoPracticoWeb.Models
{
    public class PuestosTrabajoModel(HttpClient _http, IConfiguration _configuration) : IPuestosTrabajoModel
    {

        public PuestosTrabajoRespuesta? RegistrarPuestosTrabajo(PuestosTrabajoEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PuestosTrabajo/RegistrarPuestosTrabajo";

            JsonContent body = JsonContent.Create(entidad);
            var RespuestaApi = _http.PostAsync(url, body).Result;
            if (RespuestaApi.IsSuccessStatusCode)
                return RespuestaApi.Content.ReadFromJsonAsync<PuestosTrabajoRespuesta>().Result;
            return null;
        }

        public PuestosTrabajoRespuesta? ConsultarPuestosTrabajo()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PuestosTrabajo/ConsultarPuestosTrabajo";
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PuestosTrabajoRespuesta>().Result;

            return null;
        }

        public PuestosTrabajoRespuesta? ConsultarUnPuestoTrabajo(long idEmpresa)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PuestosTrabajo/ConsultarUnPuestoTrabajo?idEmpresa=" + idEmpresa;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PuestosTrabajoRespuesta>().Result;

            return null;
        }

        public PuestosTrabajoRespuesta? ActualizarUnPuestosTrabajo(long idPuesto)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Inventario/ActualizarUnPuestosTrabajo?idPuesto=" + idPuesto;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PuestosTrabajoRespuesta>().Result;

            return null;
        }
        public PuestosTrabajoRespuesta? ActualizarPuestosTrabajo(PuestosTrabajoEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PuestosTrabajo/ActualizarPuestosTrabajo";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PuestosTrabajoRespuesta>().Result;

            return null;
        }


        public PuestosTrabajoRespuesta? EliminarPuestosTrabajo(long idPuesto)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/PuestosTrabajo/EliminarPuestosTrabajo?idPuesto=" + idPuesto;
            var resp = _http.DeleteAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PuestosTrabajoRespuesta>().Result;

            return null;
        }
    }
}