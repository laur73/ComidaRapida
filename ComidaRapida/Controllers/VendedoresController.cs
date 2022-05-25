using ComidaRapida.Models;
using ComidaRapida.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ComidaRapida.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly IRepositorioVendedores repositorioVendedores;

        public VendedoresController(IRepositorioVendedores repositorioVendedores)
        {
            this.repositorioVendedores = repositorioVendedores;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var vendedores = await repositorioVendedores.Listar();
            return View(vendedores);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteVendedor(string nombre, string usuario)
        {
            var existeHabitante = await repositorioVendedores.Existe(nombre, usuario);

            if (existeHabitante)
            {
                return Json($"El vendedor ya existe");
            }

            return Json(true);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioViewModel vendedor)
        {
            //Valida los campos del modelo
            if (!ModelState.IsValid)
            {
                return View(vendedor);
            }

            //Valida si ya existe el apoyo a insertar
            var existeVendedor = await repositorioVendedores.Existe(vendedor.Nombre, vendedor.Usuario);

            if (existeVendedor)
            {
                ModelState.AddModelError(nameof(vendedor.Nombre), $"El vendedor ya se encuentra registrado");

                return View(vendedor);
            }

            await repositorioVendedores.Crear(vendedor);

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int IdUsuario)
        {
            var vendedor = await repositorioVendedores.ObtenerId(IdUsuario);

            if (vendedor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(vendedor);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioViewModel vendedor)
        {
            var vendedorExiste = await repositorioVendedores.ObtenerId(vendedor.IdUsuario);

            if (vendedorExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioVendedores.Actualizar(vendedor);

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdUsuario)
        {
            var vendedor = await repositorioVendedores.ObtenerId(IdUsuario);

            if (vendedor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(vendedor);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(UsuarioViewModel vendedor)
        {
            await repositorioVendedores.Eliminar(vendedor.IdUsuario);
            return RedirectToAction("Listar");
        }


    }
}
