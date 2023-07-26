using Alura_JornadaMilhas.Data;
using Alura_JornadaMilhas.DTOs;
using Alura_JornadaMilhas.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Alura_JornadaMilhas.Controller;

[ApiController]
[Route("destinos")]
public class DestinoController: ControllerBase
{
    private DestinoService _service;
    private MilhasContext _context;

    public DestinoController(DestinoService service, MilhasContext context)
    {
        _service = service;
        _context = context;
    }

    // =====================================================================

    [HttpPost]
    public async Task<IActionResult> CadastraAsync([FromForm] CreateDestinoDto dto)
    {
        return await _service.CadastraAsync(dto);
    }

    [HttpGet]
    public IActionResult RecuperaTodos([FromQuery] int skip = 0, [FromQuery] int take = int.MaxValue, 
                                        [FromQuery] string? nome = null)
    {
        if (string.IsNullOrEmpty(nome))
        {
            return Ok(_context.Destinos.Skip(skip).Take(take));
        }
        
        return _service.RecuperaPorNome(nome);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaPorId(int id)
    {
        return _service.RecuperaPorId(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AlteraAsync(int id, [FromForm] CreateDestinoDto dto) {
        return await _service.Altera(id, dto);
    }

    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {
        return _service.Remove(id);
    }
}
