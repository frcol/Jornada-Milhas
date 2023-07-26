using System.ComponentModel.DataAnnotations;

namespace Alura_JornadaMilhas.DTOs;

public class DepoimentoDto
{
    [Required]
    public IFormFile Imagem { get; set; }
    [Required]
    [MaxLength(255)]
    public string Mensagem { get; set; }
    [Required]
    [MaxLength(255)]
    public string Nome { get; set; }
}
