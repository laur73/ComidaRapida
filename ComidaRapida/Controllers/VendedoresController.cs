using Microsoft.AspNetCore.Mvc;

namespace ComidaRapida.Controllers
{
    public class VendedoresController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
    }
}
