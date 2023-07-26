using Alura_JornadaMilhas.Data;
using Alura_JornadaMilhas.DTOs;
using Alura_JornadaMilhas.Models;
using Alura_JornadaMilhas.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alura_JornadaMilhas.Services;

public class DepoimentoService
{
    private IMapper _mapper;
    private MilhasContext _context;
    private IWebHostEnvironment _hostingEnvironment;

    public DepoimentoService(IMapper mapper, MilhasContext context, IWebHostEnvironment hostingEnvironment)
    {
        _mapper = mapper;
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<IActionResult> CadastraAsync(DepoimentoDto dto)
    {
        try
        {
            if (dto.Imagem == null || dto.Imagem.Length == 0)
            {
                return new BadRequestObjectResult("Nenhuma imagem enviada.");
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Imagem.FileName)}";
            await Arquivo.SalvarImagemAsync(fileName, dto.Imagem, _hostingEnvironment);

            Depoimento depoimento = _mapper.Map<Depoimento>(dto);

            depoimento.ImageUrl = "/images/" + fileName;

            _context.Depoimentos.Add(depoimento);
            _context.SaveChanges();

            return new CreatedAtActionResult("RecuperaPorId", "Depoimento", new { id = depoimento.Id }, depoimento);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    public IActionResult RecuperaPorId(int id)
    {
        Depoimento? depoimento = _context.Depoimentos.FirstOrDefault(depoimento => depoimento.Id == id);
        if (depoimento is null) { return new NotFoundObjectResult(depoimento); }

        return new OkObjectResult(depoimento);
    }



    public IActionResult RecuperaDepoimentosHome()
    {
        string sqlQuery = "SELECT * FROM milhas.depoimentos as dep ORDER BY RAND() LIMIT 3";

        var depoimentosRandomicos = _context.Depoimentos
                                    .FromSqlRaw(sqlQuery) // Executa a consulta SQL customizada
                                    .ToList();
        return new OkObjectResult(depoimentosRandomicos);
    }

    public async Task<IActionResult> AlteraDepoimentoAsync(int id, DepoimentoDto dto)
    {
        try
        {
            if (dto.Imagem == null || dto.Imagem.Length == 0)
            {
                return new BadRequestObjectResult("Nenhuma imagem enviada.");
            }

            Depoimento? depoimento = _context.Depoimentos.FirstOrDefault(depoimento => depoimento.Id == id);
            if (depoimento is null) { return new NotFoundObjectResult(depoimento); }

            string fileName = depoimento.ImageUrl.Substring(depoimento.ImageUrl.LastIndexOf('/') + 1);
            await Arquivo.SalvarImagemAsync(fileName, dto.Imagem, _hostingEnvironment);

            _mapper.Map(dto, depoimento);
            _context.SaveChanges();

            return new OkObjectResult(depoimento);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        } 
    }

    public IActionResult RemoveDepoimento(int id)
    {
        Depoimento? depoimento = _context.Depoimentos.FirstOrDefault(depoimento => depoimento.Id == id);

        if (depoimento is null) { return new NotFoundObjectResult(depoimento); }

        // Apago imagem da pasta
        string fileName = depoimento.ImageUrl.Substring(depoimento.ImageUrl.LastIndexOf('/') + 1);
        ObjectResult resultado = (ObjectResult)Arquivo.ApagaImagem(fileName, _hostingEnvironment);

        if (resultado.StatusCode == 500)
        {
            return new BadRequestObjectResult(resultado.Value);
        }

        // Apago registro no DB
        _context.Depoimentos.Remove(depoimento);
        _context.SaveChanges();

        return new OkObjectResult($"{depoimento.Id} foi removido");
    }
}
