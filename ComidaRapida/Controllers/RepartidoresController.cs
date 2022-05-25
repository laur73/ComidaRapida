using ComidaRapida.Models;
using ComidaRapida.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ComidaRapida.Controllers
{
    public class RepartidoresController : Controller
    {
        private readonly IRepositorioRepartidores repositorioRepartidores;

        public RepartidoresController(IRepositorioRepartidores repositorioRepartidores)
        {
            this.repositorioRepartidores = repositorioRepartidores;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var repartidores = await repositorioRepartidores.Listar();
            return View(repartidores);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteVendedor(string nombre, string usuario)
        {
            var existeRepartidor = await repositorioRepartidores.Existe(nombre, usuario);

            if (existeRepartidor)
            {
                return Json($"El repartidor ya existe");
            }

            return Json(true);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioViewModel repartidor)
        {
            //Valida los campos del modelo
            if (!ModelState.IsValid)
            {
                return View(repartidor);
            }

            //Valida si ya existe el apoyo a insertar
            var existeVendedor = await repositorioRepartidores.Existe(repartidor.Nombre, repartidor.Usuario);

            if (existeVendedor)
            {
                ModelState.AddModelError(nameof(repartidor.Nombre), $"El vendedor ya se encuentra registrado");

                return View(repartidor);
            }

            await repositorioRepartidores.Crear(repartidor);

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int IdUsuario)
        {
            var repartidor = await repositorioRepartidores.ObtenerId(IdUsuario);

            if (repartidor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(repartidor);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioViewModel repartidor)
        {
            var vendedorExiste = await repositorioRepartidores.ObtenerId(repartidor.IdUsuario);

            if (vendedorExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioRepartidores.Actualizar(repartidor);

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdUsuario)
        {
            var repartidor = await repositorioRepartidores.ObtenerId(IdUsuario);

            if (repartidor is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(repartidor);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(UsuarioViewModel repartidor)
        {
            await repositorioRepartidores.Eliminar(repartidor.IdUsuario);
            return RedirectToAction("Listar");
        }
    }
}
