using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models;

public class Gasto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "La Fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Today;


    [DataType(DataType.Currency)]
    [Required(ErrorMessage = "El Valor es obligatorio")]
    public decimal Valor { get; set; }


    [Range(1, int.MaxValue, ErrorMessage = "Elija una Categoria")]
    [Display(Name = "Categoria")]
    [Required(ErrorMessage = "La Categoria es obligatoria")]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }

    [NotMapped]
    public string CategoriaNome { get; set; } = string.Empty;
}