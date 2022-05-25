﻿using ComidaRapida.Models;
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
    }
}