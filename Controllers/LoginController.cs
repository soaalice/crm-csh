using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace crm_csh.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _loginUrl = "http://localhost:8080/api/login";
        private readonly string _logoutApiUrl = "http://localhost:8080/api/logout";

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };

            var content = new FormUrlEncodedContent(keyValues);

            try
            {
                var response = await _httpClient.PostAsync(_loginUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Connexion de l'utilisateur
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Dashboard");
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new Exception("API introuvable");
                } else {
                    throw new Exception("Identifiant ou mot de passe invalide");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Une erreur s'est produite : " + ex.Message;
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var response = await _httpClient.PostAsync(_logoutApiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewData["Error"] = "Erreur lors de la déconnexion de l'API.";
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Une erreur s'est produite : " + ex.Message;
                return RedirectToAction("Index", "Dashboard");
            }
        }
    }
}
