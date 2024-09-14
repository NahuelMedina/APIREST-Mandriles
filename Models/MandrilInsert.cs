using System.ComponentModel.DataAnnotations;

namespace MandrilAPI.Models;

public class MandrilInsert
{
    [Required]
    [MaxLength(20)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string Apellido { get; set; } = string.Empty;
}
