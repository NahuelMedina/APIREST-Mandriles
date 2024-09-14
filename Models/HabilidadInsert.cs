using static MandrilAPI.Models.Habilidad;

namespace MandrilAPI.Models;

public class HabilidadInsert
{
    public string Nombre { get; set; } = string.Empty;

    public Epotencia Potencia { get; set; }
}
