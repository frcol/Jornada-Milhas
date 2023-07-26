using Microsoft.AspNetCore.Mvc;

namespace Alura_JornadaMilhas.Utilities;

public class Arquivo
{

    // ===================================================================================
    public static async Task SalvarImagemAsync(string fileName, IFormFile img, IWebHostEnvironment _hostingEnvironment)
    {
        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);

        // Salva a imagem no disco
        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await img.CopyToAsync(stream);
        }
    }

    public static IActionResult ApagaImagem(string fileName, IWebHostEnvironment _hostingEnvironment)
    {
        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);

        try
        {
            // Verifica se o arquivo existe antes de tentar excluir
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return new OkObjectResult("Imagem excluída com sucesso.");
            }
            else
            {
                return new OkObjectResult("Imagem não encontrada.");
            }
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}
