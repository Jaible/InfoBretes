using InfoBreteWeb.Entities;

namespace InfoBreteWeb.Models
{
    public class EmpresasModel(HttpClient http, IConfiguration iConfiguration, HttpContextAccessor iAccesor) : IEmpresasModel
    {
        public Respuesta RegistrarEmpresas(Empresas ent)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Empresas/RegistrarEmpresas";
            JsonContent body = JsonContent.Create(ent);
            var result = http.PostAsync(url, body).Result;

            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }
    }
}
