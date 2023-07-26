using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alura_JornadaMilhas.Models;

public class Destino
{
    [Key]
    public int Id { get; set; }
    public string ImageUlr   { get; set; }
    public string Nome { get; set;}
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }
}