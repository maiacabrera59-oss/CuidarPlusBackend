using CuidarPlusAPI.DTOs;
using CuidarPlusAPI.Models;
using CuidarPlusAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuidarPlusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistorialesAnimoController : ControllerBase
{
    private readonly CuidarPlusContext _context;

    public HistorialesAnimoController(CuidarPlusContext context)
    {
        _context = context;
    }

    // GET: api/historialesanimo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistorialAnimoDto>>> GetAll()
    {
        var historiales = await _context.HistorialesAnimo
            .Select(h => new HistorialAnimoDto
            {
                IdHistorialAnimo = h.IdHistorialAnimo,
                Fecha = h.Fecha,
                Hora = h.Hora,
                Observaciones = h.Observaciones,
                IdUsuario = h.IdUsuario,
                IdEstado = h.IdEstado
            })
            .ToListAsync();

        return Ok(historiales);
    }

    // GET: api/historialesanimo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HistorialAnimoDto>> GetById(int id)
    {
        var historial = await _context.HistorialesAnimo.FindAsync(id);

        if (historial is null)
            return NotFound();

        var dto = new HistorialAnimoDto
        {
            IdHistorialAnimo = historial.IdHistorialAnimo,
            Fecha = historial.Fecha,
            Hora = historial.Hora,
            Observaciones = historial.Observaciones,
            IdUsuario = historial.IdUsuario,
            IdEstado = historial.IdEstado
        };

        return Ok(dto);
    }

    // POST: api/historialesanimo
    [HttpPost]
    public async Task<ActionResult<HistorialAnimoDto>> Create(HistorialAnimoCrearDto dto)
    {
        var historial = new HistorialAnimo
        {
            Fecha = dto.Fecha,
            Hora = dto.Hora,
            Observaciones = dto.Observaciones,
            IdUsuario = dto.IdUsuario,
            IdEstado = dto.IdEstado
        };

        _context.HistorialesAnimo.Add(historial);
        await _context.SaveChangesAsync();

        var result = new HistorialAnimoDto
        {
            IdHistorialAnimo = historial.IdHistorialAnimo,
            Fecha = historial.Fecha,
            Hora = historial.Hora,
            Observaciones = historial.Observaciones,
            IdUsuario = historial.IdUsuario,
            IdEstado = historial.IdEstado
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = historial.IdHistorialAnimo },
            result
        );
    }

    // PUT: api/historialesanimo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, HistorialAnimoActualizarDto dto)
    {
        var historial = await _context.HistorialesAnimo.FindAsync(id);

        if (historial is null)
            return NotFound();

        historial.Fecha = dto.Fecha;
        historial.Hora = dto.Hora;
        historial.Observaciones = dto.Observaciones;
        historial.IdUsuario = dto.IdUsuario;
        historial.IdEstado = dto.IdEstado;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/historialesanimo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var historial = await _context.HistorialesAnimo.FindAsync(id);

        if (historial is null)
            return NotFound();

        _context.HistorialesAnimo.Remove(historial);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}