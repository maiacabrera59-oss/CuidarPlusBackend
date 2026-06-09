using CuidarPlusAPI.DTOs;
using CuidarPlusAPI.Models;
using CuidarPlusAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuidarPlusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordatoriosController : ControllerBase
{
    private readonly CuidarPlusContext _context;

    public RecordatoriosController(CuidarPlusContext context)
    {
        _context = context;
    }

    // GET: api/recordatorios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecordatorioDto>>> GetAll()
    {
        var recordatorios = await _context.Recordatorios
            .Select(r => new RecordatorioDto
            {
                IdRecordatorio = r.IdRecordatorio,
                Canal = r.Canal,
                FechaHoraProgramada = r.FechaHoraProgramada
            })
            .ToListAsync();

        return Ok(recordatorios);
    }

    // GET: api/recordatorios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RecordatorioDto>> GetById(int id)
    {
        var recordatorio = await _context.Recordatorios.FindAsync(id);

        if (recordatorio is null)
            return NotFound();

        var dto = new RecordatorioDto
        {
            IdRecordatorio = recordatorio.IdRecordatorio,
            Canal = recordatorio.Canal,
            FechaHoraProgramada = recordatorio.FechaHoraProgramada
        };

        return Ok(dto);
    }

    // POST: api/recordatorios
    [HttpPost]
    public async Task<ActionResult<RecordatorioDto>> Create(RecordatorioCrearDto dto)
    {
        var recordatorio = new Recordatorio
        {
            Canal = dto.Canal,
            FechaHoraProgramada = dto.FechaHoraProgramada
        };

        _context.Recordatorios.Add(recordatorio);
        await _context.SaveChangesAsync();

        var result = new RecordatorioDto
        {
            IdRecordatorio = recordatorio.IdRecordatorio,
            Canal = recordatorio.Canal,
            FechaHoraProgramada = recordatorio.FechaHoraProgramada
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = recordatorio.IdRecordatorio },
            result
        );
    }

    // PUT: api/recordatorios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RecordatorioActualizarDto dto)
    {
        var recordatorio = await _context.Recordatorios.FindAsync(id);

        if (recordatorio is null)
            return NotFound();

        recordatorio.Canal = dto.Canal;
        recordatorio.FechaHoraProgramada = dto.FechaHoraProgramada;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/recordatorios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var recordatorio = await _context.Recordatorios.FindAsync(id);

        if (recordatorio is null)
            return NotFound();

        _context.Recordatorios.Remove(recordatorio);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}