using System.ComponentModel.DataAnnotations;

namespace Alura_JornadaMilhas.Models;

public class Depoimento
{
    [Key]
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    [Required]
    [MaxLength(255)]
    public string Mensagem { get; set; }
    [Required]
    [MaxLength(255)]
    public string Nome { get; set; }
}
