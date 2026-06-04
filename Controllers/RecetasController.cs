using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuidarPlusAPI.Models;
using CuidarPlusAPI.DTOs;
using CuidarPlusAPI.Data;

namespace CuidarPlusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecetaController : ControllerBase
{
    private readonly CuidarPlusContext _context;

    public RecetaController(CuidarPlusContext context)
    {
        _context = context;
    }

    // GET: api/Receta
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecetaDto>>> GetAll()
    {
        var recetas = await _context.Recetas
            .Select(r => new RecetaDto
            {
                IdReceta = r.IdReceta,
                Archivos = r.Archivos,
                Observaciones = r.Observaciones,
                IdMedico = r.IdMedico
            })
            .ToListAsync();

        return Ok(recetas);
    }

    // GET: api/Receta/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RecetaDto>> GetById(int id)
    {
        var receta = await _context.Recetas.FindAsync(id);

        if (receta is null)
            return NotFound();

        var dto = new RecetaDto
        {
            IdReceta = receta.IdReceta,
            Archivos = receta.Archivos,
            Observaciones = receta.Observaciones,
            IdMedico = receta.IdMedico
        };

        return Ok(dto);
    }

    // POST: api/Receta
    [HttpPost]
    public async Task<ActionResult<RecetaDto>> Create([FromBody] RecetaCrearDto crearDto)
    {
        var receta = new Receta
        {
            Archivos = crearDto.Archivos,
            Observaciones = crearDto.Observaciones,
            IdMedico = crearDto.IdMedico
        };

        _context.Recetas.Add(receta);
        await _context.SaveChangesAsync();

        var dto = new RecetaDto
        {
            IdReceta = receta.IdReceta,
            Archivos = receta.Archivos,
            Observaciones = receta.Observaciones,
            IdMedico = receta.IdMedico
        };

        return CreatedAtAction(nameof(GetById), new { id = receta.IdReceta }, dto);
    }

    // PUT: api/Receta/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RecetaActualizarDto actualizarDto)
    {
        var receta = await _context.Recetas.FindAsync(id);

        if (receta is null)
            return NotFound();

        receta.Archivos = actualizarDto.Archivos;
        receta.Observaciones = actualizarDto.Observaciones;
        receta.IdMedico = actualizarDto.IdMedico;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Receta/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var receta = await _context.Recetas.FindAsync(id);

        if (receta is null)
            return NotFound();

        _context.Recetas.Remove(receta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}