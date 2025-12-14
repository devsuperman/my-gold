using System.ComponentModel.DataAnnotations;

namespace App3.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    public string Nombre { get; set; } = string.Empty;
}