﻿using Microsoft.AspNetCore.Mvc;

namespace ComidaRapida.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
