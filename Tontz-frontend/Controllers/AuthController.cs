using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Tontz_frontend.Models;

namespace Tontz_frontend.Controllers {
    public class AuthController : Controller {
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory httpClientFactory) {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (!ModelState.IsValid) {
                TempData["Message"] = "Digite o usuário e senha!";
                return View(model);
            }

            Console.WriteLine($"Username: {model.Username}");
            Console.WriteLine($"Password: {model.Password}");

            var json = JsonSerializer.Serialize(new {
                username = model.Username,
                password = model.Password
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // BaseAddress já configurado, só passa o endpoint relativo
            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode) {
                TempData["Message"] = "Usuário ou senha incorretos!";
                return View(model);
            }

            var result = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(result);

            HttpContext.Session.SetString("JWToken", tokenObj["token"]);

            // Se deu certo, podemos usar TempData para alert de sucesso
            TempData["Message"] = "Login realizado com sucesso!";
            return View(model);
        }
    }
}
