using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/gastos")]
public class GastosController(IGastosRepository repository) : ControllerBase
{
    private readonly IGastosRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll(int categoriaId = 0, DateTime? mesAno = null)
    {
        mesAno ??= DateTime.Today;
        var lista = await _repository.ListAll(mesAno.Value);
        return Ok(lista);
    }
    
    [HttpGet("ListarPorCategoria")]
    public async Task<IActionResult> ListarPorCategoria(DateTime mesAno)
    {
        var lista = await _repository.ListarPorCategoria(mesAno);
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var model = await _repository.Get(id);
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(Gasto model)
    {
        if (ModelState.IsValid)
        {
            await _repository.Upsert(model);
            return Ok(model);
        }
        return Ok(ModelState);
    }
}
