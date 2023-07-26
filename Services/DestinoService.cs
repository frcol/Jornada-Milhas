using Alura_JornadaMilhas.Data;
using Alura_JornadaMilhas.DTOs;
using Alura_JornadaMilhas.Models;
using Alura_JornadaMilhas.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alura_JornadaMilhas.Services;

public class DestinoService
{
    private IWebHostEnvironment _environment;
    private MilhasContext _context;
    private IMapper _mapper;

    public DestinoService(IWebHostEnvironment environment, MilhasContext context, IMapper mapper)
    {
        _environment = environment;
        _context = context;
        _mapper = mapper;
    }

    //============================================================================
    public async Task<IActionResult> CadastraAsync(CreateDestinoDto dto)
    {
        try
        {
            if (dto.Imagem is null) { return new BadRequestObjectResult("Imagem URL é obrigatório"); }

            string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(dto.Imagem.FileName)}";
            await Arquivo.SalvarImagemAsync(fileName, dto.Imagem, _environment);

            Destino destino = _mapper.Map<Destino>(dto);
            destino.ImageUlr = "/images/" + fileName;

            _context.Destinos.Add(destino);
            _context.SaveChanges();

            return new CreatedAtActionResult("RecuperaPorId", "Destino", new { id = destino.Id}, destino);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    public async Task<IActionResult> Altera(int id, CreateDestinoDto dto)
    {
        try
        {
            if (dto.Imagem is null) { return new BadRequestObjectResult("Imagem URL é obrigatório"); }

            Destino? destino = _context.Destinos.FirstOrDefault(destino => destino.Id == id);
            if (destino is null) { return new NotFoundObjectResult(dto); }

            string fileName = destino.ImageUlr.Substring(destino.ImageUlr.LastIndexOf('/') + 1);
            await Arquivo.SalvarImagemAsync(fileName, dto.Imagem, _environment);

            _mapper.Map(dto, destino);
            _context.SaveChanges();

            return new OkObjectResult(destino);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    public IActionResult RecuperaPorId(int id)
    {
        Destino? destino = _context.Destinos.FirstOrDefault(destino => destino.Id == id);
        if (destino is null) { return new NotFoundObjectResult(id); }

        return new OkObjectResult(destino);
    }

    public IActionResult Remove(int id)
    {
        Destino? destino = _context.Destinos.FirstOrDefault(destino => destino.Id == id);
        if (destino is null) { return new NotFoundObjectResult(id); }

        string fileName = destino.ImageUlr.Substring(destino.ImageUlr.LastIndexOf('/') + 1);
        Arquivo.ApagaImagem(fileName, _environment);

        _context.Destinos.Remove(destino);

        return new OkObjectResult($"{destino.Id} foi removido");
    }

    public IActionResult RecuperaPorNome(string nome)
    {
        IEnumerable<Destino> destinos = (from destino in _context.Destinos
                                        where destino.Nome.Contains(nome)
                                        select destino).ToList();

        if (!destinos.Any()) { return new NotFoundObjectResult(new {mensagem= "Nenhum destino foi encontrado" }); }

        return new OkObjectResult(destinos);
    }
}
