using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ITS_Anchor.Models;

using System.Net.Http;
using System.Text;
using System.Web;

namespace ITS_Anchor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<String> enviarSuge(string sug)
        {
            using (var client = new HttpClient())
            {
                var jsonData = System.Text.Json.JsonSerializer.Serialize(new
                {
                    sugerencia=sug
                });
                var content = new StringContent(jsonData);
                content.Headers.ContentType.CharSet= string.Empty;
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("https://prod-16.westus.logic.azure.com:443/workflows/99a2a438fbc644febdc5570fc14ca57b/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=si0j9WOtp3FvdCkiQ50bH6pBbLup9RZ132UCtYqtW4g",content);

                //return result.StatusCode.ToString();

                var respuesta = "Sugerencia recibida";
                return respuesta;
            }
        }

        public async Task<String> enviarContacto(string ape, string cel, string correo, string msj, string nom)
        {
            using (var client = new HttpClient())
            {
                var jsonData = System.Text.Json.JsonSerializer.Serialize(new
                {
                    apellidos=ape,
                    celular=cel,
                    email=correo,
                    mensaje=msj,
                    nombres=nom
                });
                var content = new StringContent(jsonData);
                content.Headers.ContentType.CharSet= string.Empty;
                content.Headers.ContentType.MediaType = "application/json";

                var response = await client.PostAsync("https://prod-100.westus.logic.azure.com:443/workflows/e86a9178840246ce8b5fcbf82e5f12e0/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=GpPDGBq1wa8ZsGPy1KwJywPhh2KgxRqNPV4zMiYyNBw",content);

                //return result.StatusCode.ToString();
                
                var respuesta = "Estaremos en contacto";
                return respuesta;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
