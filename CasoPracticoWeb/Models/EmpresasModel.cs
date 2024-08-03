using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using static CasoPracticoWeb.Entities.EmpresasEnt;


namespace CasoPracticoWeb.Models
{
    public class EmpresasModel(HttpClient _http, IConfiguration _configuration) : IEmpresasModel
    {

        public EmpresasRespuesta? RegistrarEmpresas(EmpresasEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empresas/RegistrarEmpresas";

            JsonContent body = JsonContent.Create(entidad);
            var RespuestaApi = _http.PostAsync(url, body).Result;
            if (RespuestaApi.IsSuccessStatusCode)
                return RespuestaApi.Content.ReadFromJsonAsync<EmpresasRespuesta>().Result;
            return null;
        }

        public EmpresasRespuesta? ConsultarEmpresas()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empresas/ConsultarEmpresas";
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<EmpresasRespuesta>().Result;

            return null;
        }

    }
}