using InfoBretesWeb.Entities;
using InfoBretesWeb.Services;
using System.Text.Json;
using static InfoBretesWeb.Entities.UserEnt;

namespace InfoBretesWeb.Models
{
    public class UserModel(IConfiguration _configuration, HttpClient _httpClient) : IUserModel
    {
        public UserRespuesta IniciarSesion(Login user)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Users/login";

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<UserRespuesta>().Result!;
            }

            return new UserRespuesta();
        }

        public UserRespuesta RegistrarUsuario(UserEnt user)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Users/register";

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<UserRespuesta>().Result!;
            }

            return new UserRespuesta();
        }

        public UserRespuesta Perfil(UserEnt user)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Users/profile?email=" + user.Email;
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<UserRespuesta>().Result!;
            }

            return new UserRespuesta();
        }

        public UserRespuesta ModificaPerfil(UserEnt user)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Users/update?email=" + user.Email;
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<UserRespuesta>().Result!;
            }

            return new UserRespuesta();
        }

        public UserRespuesta ActualizarUsuario(UserEnt user)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Users/update";

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<UserRespuesta>().Result!;
            }

            return new UserRespuesta();
        }
    }
}
