using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuidarPlusAPI.Models;
using CuidarPlusAPI.DTOs;
using CuidarPlusAPI.Data;

namespace CuidarPlusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistroTomaController : ControllerBase
{
    private readonly CuidarPlusContext _context;

    public RegistroTomaController(CuidarPlusContext context)
    {
        _context = context;
    }

    // GET: api/RegistroToma
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistroTomaDto>>> GetAll()
    {
        var registros = await _context.RegistroTomas
            .Select(r => new RegistroTomaDto
            {
                IdRegistroToma = r.IdRegistroToma,
                Estado = r.Estado,
                FechaHoraReal = r.FechaHoraReal,
                Observaciones = r.Observaciones,
                IdRecordatorio = r.IdRecordatorio,
                IdHistorialAnimo = r.IdHistorialAnimo
            })
            .ToListAsync();

        return Ok(registros);
    }

    // GET: api/RegistroToma/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RegistroTomaDto>> GetById(int id)
    {
        var registro = await _context.RegistroTomas.FindAsync(id);

        if (registro is null)
            return NotFound();

        var dto = new RegistroTomaDto
        {
            IdRegistroToma = registro.IdRegistroToma,
            Estado = registro.Estado,
            FechaHoraReal = registro.FechaHoraReal,
            Observaciones = registro.Observaciones,
            IdRecordatorio = registro.IdRecordatorio,
            IdHistorialAnimo = registro.IdHistorialAnimo
        };

        return Ok(dto);
    }

    // POST: api/RegistroToma
    [HttpPost]
    public async Task<ActionResult<RegistroTomaDto>> Create([FromBody] RegistroTomaCrearDto crearDto)
    {
        var registro = new RegistroToma
        {
            Estado = crearDto.Estado,
            FechaHoraReal = crearDto.FechaHoraReal,
            Observaciones = crearDto.Observaciones,
            IdRecordatorio = crearDto.IdRecordatorio,
            IdHistorialAnimo = crearDto.IdHistorialAnimo
        };

        _context.RegistroTomas.Add(registro);
        await _context.SaveChangesAsync();

        var dto = new RegistroTomaDto
        {
            IdRegistroToma = registro.IdRegistroToma,
            Estado = registro.Estado,
            FechaHoraReal = registro.FechaHoraReal,
            Observaciones = registro.Observaciones,
            IdRecordatorio = registro.IdRecordatorio,
            IdHistorialAnimo = registro.IdHistorialAnimo
        };

        return CreatedAtAction(nameof(GetById), new { id = registro.IdRegistroToma }, dto);
    }

    // PUT: api/RegistroToma/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RegistroTomaActualizarDto actualizarDto)
    {
        var registro = await _context.RegistroTomas.FindAsync(id);

        if (registro is null)
            return NotFound();

        registro.Estado = actualizarDto.Estado;
        registro.FechaHoraReal = actualizarDto.FechaHoraReal;
        registro.Observaciones = actualizarDto.Observaciones;
        registro.IdRecordatorio = actualizarDto.IdRecordatorio;
        registro.IdHistorialAnimo = actualizarDto.IdHistorialAnimo;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/RegistroToma/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var registro = await _context.RegistroTomas.FindAsync(id);

        if (registro is null)
            return NotFound();

        _context.RegistroTomas.Remove(registro);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}