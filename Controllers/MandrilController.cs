using MandrilAPI.Helpers;
using MandrilAPI.Models;
using MandrilAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MandrilAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MandrilController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Mandril>> GetMandriles()
    {
        // Obtengo todos los mandriles desde el store
        var mandriles = MandrilDataStore.Current.Mandriles;
        return Ok(mandriles);
    }

    [HttpGet("{mandrilId}")]
    public ActionResult<Mandril> GetMandril(int mandrilId)
    {
        // 
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        return Ok(mandril);
    }

    [HttpPost]
    public ActionResult<Mandril> PostMandril([FromBody] MandrilInsert mandrilInsert)
    {
        var mandriles = MandrilDataStore.Current.Mandriles;

        // Verifico si hay mandriles en la lista si no instancio maxMandrilId en 0
        var maxMandrilId = mandriles.Any() ? mandriles.Max(x => x.Id) : 0;

        // Intancio un nuevo mandril para agregar a la lista
        var mandrilNuevo = new Mandril()
        {
            Id = maxMandrilId + 1,
            Nombre = mandrilInsert.Nombre,
            Apellido = mandrilInsert.Apellido
        };

        // Agrego el nuevo mandril a la lista
        MandrilDataStore.Current.Mandriles.Add(mandrilNuevo);

        // Retorno el mandril creado
        return CreatedAtAction(nameof(GetMandril),
            new { mandrilId = mandrilNuevo.Id },
            mandrilNuevo
        );
    }

    [HttpPut("{mandrilId}")]
    public ActionResult<Mandril> PutMandril([FromRoute] int mandrilId, [FromBody] MandrilInsert mandrilInsert)
    {
        // Validacion
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        // Asigno los nuevos valores pasados por parametros
        mandril.Nombre = mandrilInsert.Nombre;
        mandril.Apellido = mandrilInsert.Apellido;

        return NoContent();
    }

    [HttpDelete("{mandrilId}")]
    public ActionResult<Mandril> DeleteMandril(int mandrilId)
    {
        // Validacion
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        // Elimino el mandril de la lista
        MandrilDataStore.Current.Mandriles.Remove(mandril);

        return NoContent();
    }
}