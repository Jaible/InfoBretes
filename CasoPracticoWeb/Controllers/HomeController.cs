using CasoPracticoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using InfoBretesWeb.Entities;
using InfoBretesWeb.Models;
using static InfoBretesWeb.Entities.UserEnt;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CasoPracticoWeb.Controllers
{
    public class HomeController(IUserModel iUserModel) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = new Login
            {
                Email = email,
                Password = password
            };

            UserRespuesta respuesta = iUserModel.IniciarSesion(user);

            if (respuesta.Codigo == "1") {
                List<Claim> claims = new List<Claim>()
                {
                    new(ClaimTypes.Email, email)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties() { AllowRefresh = true };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    properties
                );
                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost("register")]
        public IActionResult Register(string nombre, string email, string password)
        {
            var user = new UserEnt { Email = email, Nombre = nombre, Password = password };
            UserRespuesta respuesta = iUserModel.RegistrarUsuario(user);

            if (respuesta.Codigo == "1")
            {
                return RedirectToAction("Login");
            } else
            {
                ViewBag.Mensaje = respuesta.Mensaje;
                return View();
            }
            
        }

        [HttpGet("profile")]
        public IActionResult Profile()
        {
            UserEnt user = new UserEnt { Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() };
            var respuesta = iUserModel.Perfil(user);

            if (respuesta.Codigo == "1")
            {
                ViewData.Model = respuesta.Dato;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet("updateprofile")]
        public IActionResult UpdateProfile()
        {
            UserEnt user = new UserEnt { Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() };
            var respuesta = iUserModel.ModificaPerfil(user);

            if (respuesta.Codigo == "1")
            {
                ViewData.Model = respuesta.Dato;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("updateprofile")]
        public async Task<IActionResult> UpdateProfile(UserEnt user)
        {
            var respuesta = iUserModel.ActualizarUsuario(user);

            if (respuesta.Codigo == "1")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new(ClaimTypes.Email, user.Email)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties() { AllowRefresh = true };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    properties
                );
                return RedirectToAction("Profile");
            }
            else
            {
                return RedirectToAction("UpdateProfile");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
