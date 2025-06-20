using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Repositories;
using Dominio.Models;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/gastos")]
public class GastosController(GastosRepository repository) : ControllerBase
{
    private readonly GastosRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll(DateTime? mesAno = null)
    {
        mesAno ??= DateTime.Today;
        var lista = await _repository.ListAll(mesAno.Value);
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
