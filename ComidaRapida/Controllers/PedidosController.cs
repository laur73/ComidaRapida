using ComidaRapida.Models;
using ComidaRapida.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ComidaRapida.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IRepositorioPedidos repositorioPedidos;

        public PedidosController(IRepositorioPedidos repositorioPedidos)
        {
            this.repositorioPedidos = repositorioPedidos;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var pedidos = await repositorioPedidos.Listar();
            return View(pedidos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PedidoViewModel pedido)
        {
            //Valida los campos del modelo
            if (!ModelState.IsValid)
            {
                return View(pedido);
            }

            await repositorioPedidos.Crear(pedido);

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int IdPedido)
        {
            var pedido = await repositorioPedidos.ObtenerId(IdPedido);

            if (pedido is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(pedido);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(PedidoViewModel pedido)
        {
            var pedidoExiste = await repositorioPedidos.ObtenerId(pedido.IdPedido);

            if (pedidoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPedidos.Actualizar(pedido);

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdPedido)
        {
            var pedido = await repositorioPedidos.ObtenerId(IdPedido);

            if (pedido is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(PedidoViewModel pedido)
        {
            await repositorioPedidos.Eliminar(pedido.IdPedido);
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Entregar(int IdPedido)
        {
            var pedido = await repositorioPedidos.ObtenerId(IdPedido);

            if (pedido is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(pedido);

        }

        [HttpPost]
        public async Task<IActionResult> Entregar(PedidoViewModel pedido)
        {
            var pedidoExiste = await repositorioPedidos.ObtenerId(pedido.IdPedido);

            if (pedidoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioPedidos.Entregar(pedido);

            return RedirectToAction("Listar");
        }
    }
}
