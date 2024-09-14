using System.Runtime.CompilerServices;
using MandrilAPI.Helpers;
using MandrilAPI.Models;
using MandrilAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MandrilAPI.Controllers;

[ApiController]
[Route("/api/mandril/{mandrilId}/[controller]")]
public class HabilidadController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Habilidad>> GetHabilidades(int mandrilId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);

        // Verifico si existe un mandril con el ID pasado por parametros    
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        // Retorno las habilidades del mandril existente
        return Ok(mandril.Habilidades);
    }

    [HttpGet("{habilidadId}")]
    public ActionResult<Habilidad> GetHabilidad(int mandrilId, int habilidadId)
    {
        // Verifico si existe un mandril con el ID pasado por parametros  
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        // Verifico si existe una habilidad con el ID pasado por parametros
        var habilidad = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);
        if (habilidad == null)
            return NotFound(Mensajes.Habilidad.NotFound);

        // Retorno la habilidad existente
        return Ok(habilidad);
    }

    [HttpPost]
    public ActionResult<Habilidad> PostHabilidad(int mandrilId, HabilidadInsert habilidadInsert)
    {
        // Verifico si existe un mandril con el ID pasado por parametros
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        // Verifico si ya existe una habilidad con el nombre pasado por parametros
        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Nombre == habilidadInsert.Nombre);
        if (habilidadExistente != null)
            return BadRequest(Mensajes.Habilidad.NombreExistente);

        // Si no tiene habilidades instancio con valor 0 a maxHabilidad si no toma el mayor de los IDs
        var maxHabilidad = mandril.Habilidades.Any() ? mandril.Habilidades.Max(h => h.Id) : 0;

        // Instancio una nueva habilidad para agregar al mandril
        var habilidadNueva = new Habilidad()
        {
            Id = maxHabilidad + 1,
            Nombre = habilidadInsert.Nombre,
            Potencia = habilidadInsert.Potencia
        };

        // Agrego la nueva habilidad al Mandril con .Add
        mandril.Habilidades?.Add(habilidadNueva);

        // Retorno la habilidad creada
        return CreatedAtAction(nameof(GetHabilidad),
            new { mandrilId = mandrilId, habilidadId = habilidadNueva.Id },
            habilidadNueva
            );
    }

    [HttpPut("{habilidadId}")]
    public ActionResult<Habilidad> PutHabilidad(int mandrilId, int habilidadId, HabilidadInsert habilidadInsert)
    {
        // Verifico si existe un mandril con el ID pasado por parametros
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        // Verifico si ya existe una habilidad con el nombre pasado por parametros
        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Nombre == habilidadInsert.Nombre);
        if (habilidadExistente != null)
            return BadRequest(Mensajes.Habilidad.NotFound);

        // Verifico si otro ID tiene el mismo nombre que el pasado por parametros para la habilidad
        var habilidadMismoNombre = mandril.Habilidades?
            .FirstOrDefault(h => h.Id != habilidadId && h.Nombre == habilidadInsert.Nombre);
        if (habilidadMismoNombre != null)
            return BadRequest(Mensajes.Habilidad.NombreExistente);

        // Busco la habilidad a cambiar en el mandril
        var habilidadACambiar = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

        // Asigno los nuevos valores a la habilidad
        habilidadACambiar.Nombre = habilidadInsert.Nombre;
        habilidadACambiar.Potencia = habilidadInsert.Potencia;

        return NoContent();
    }

    [HttpDelete("{habilidadId}")]
    public ActionResult<Habilidad> DeleteHabilidad(int mandrilId, [FromRoute] int habilidadId)
    {
        // Validaciones
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);
        if (mandril == null)
            return NotFound(Mensajes.Mandril.NotFound);

        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);
        if (habilidadExistente == null)
            return BadRequest(Mensajes.Habilidad.NotFound);

        // Elimino la habilidad del mandril
        mandril.Habilidades?.Remove(habilidadExistente);

        return NoContent();
    }
}