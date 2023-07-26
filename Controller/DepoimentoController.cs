using Alura_JornadaMilhas.Data;
using Alura_JornadaMilhas.DTOs;
using Alura_JornadaMilhas.Models;
using Alura_JornadaMilhas.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alura_JornadaMilhas.Controller;

[ApiController]
[Route("/depoimentos")]
[DisableRequestSizeLimit]
public class DepoimentoController: ControllerBase
{

    private DepoimentoService _service;
    private MilhasContext _context;

    public DepoimentoController(DepoimentoService service, MilhasContext context)
    {
        _service = service;
        _context = context;
    }

    // ===============================================================
    [HttpPost]
    public async Task<IActionResult> Cadastra([FromForm] DepoimentoDto dto)
    {
        return await _service.CadastraAsync(dto);
    }

    [HttpGet]
    public IActionResult RecuperaTudo([FromQuery] int skip=0, [FromQuery] int take=int.MaxValue)
    {
        return Ok(_context.Depoimentos.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaPorId(int id)
    {
        return _service.RecuperaPorId(id);
    }

    [HttpGet("/depoimentos-home")]
    public IActionResult RecuperaDepoimentosHome()
    {
        return _service.RecuperaDepoimentosHome();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AlteraDepoimento(int id, [FromForm] DepoimentoDto dto) 
    {
        return await _service.AlteraDepoimentoAsync(id, dto);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveDepoimento(int id)
    {
        return _service.RemoveDepoimento(id);
    } 
}
