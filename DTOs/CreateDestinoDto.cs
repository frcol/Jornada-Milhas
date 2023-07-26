using System.ComponentModel.DataAnnotations;

namespace Alura_JornadaMilhas.DTOs;

public class CreateDestinoDto
{
    private string preco;


    [Required]
    public IFormFile Imagem { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nome { get; set; }

    [Required]
    public string Preco { 
        get { return preco; }
        set { preco = value.Replace(',','.'); } 
    }
}
