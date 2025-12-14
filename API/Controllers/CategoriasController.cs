using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Repositories;
using API.Models;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/categorias")]
public class CategoriasController(CategoriasRepository respository) : ControllerBase
{
    private readonly CategoriasRepository _repository = respository;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _repository.ListAll();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBy(int id)
    {
        var model = await _repository.Get(id);
        return Ok(model);
    }


    [HttpPost]
    public async Task<IActionResult> Post(Categoria model)
    {
        if (ModelState.IsValid)
        {
            await _repository.Upsert(model);
            return Ok(model);
        }

        return Ok(ModelState);
    }
}
