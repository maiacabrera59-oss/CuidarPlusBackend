using CuidarPlusAPI.DTOs;
using CuidarPlusAPI.Models;
using CuidarPlusAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuidarPlusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicamentosController : ControllerBase
{
    private readonly CuidarPlusContext _context;

    public MedicamentosController(CuidarPlusContext context)
    {
        _context = context;
    }

    // GET: api/medicamentos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetAll()
    {
        var medicamentos = await _context.Medicamentos
            .Select(m => new MedicamentoDto
            {
                IdMedicamento = m.IdMedicamento,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                Presentacion = m.Presentacion,
                IdLaboratorio = m.IdLaboratorio
            })
            .ToListAsync();

        return Ok(medicamentos);
    }

    // GET: api/medicamentos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MedicamentoDto>> GetById(int id)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);

        if (medicamento is null)
            return NotFound();

        var dto = new MedicamentoDto
        {
            IdMedicamento = medicamento.IdMedicamento,
            Nombre = medicamento.Nombre,
            Descripcion = medicamento.Descripcion,
            Presentacion = medicamento.Presentacion,
            IdLaboratorio = medicamento.IdLaboratorio
        };

        return Ok(dto);
    }

    // POST: api/medicamentos
    [HttpPost]
    public async Task<ActionResult<MedicamentoDto>> Create(MedicamentoCrearDto dto)
    {
        var medicamento = new Medicamento
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Presentacion = dto.Presentacion,
            IdLaboratorio = dto.IdLaboratorio
        };

        _context.Medicamentos.Add(medicamento);
        await _context.SaveChangesAsync();

        var result = new MedicamentoDto
        {
            IdMedicamento = medicamento.IdMedicamento,
            Nombre = medicamento.Nombre,
            Descripcion = medicamento.Descripcion,
            Presentacion = medicamento.Presentacion,
            IdLaboratorio = medicamento.IdLaboratorio
        };

        return CreatedAtAction(nameof(GetById), new { id = medicamento.IdMedicamento }, result);
    }

    // PUT: api/medicamentos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MedicamentoActualizarDto dto)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);

        if (medicamento is null)
            return NotFound();

        medicamento.Nombre = dto.Nombre;
        medicamento.Descripcion = dto.Descripcion;
        medicamento.Presentacion = dto.Presentacion;
        medicamento.IdLaboratorio = dto.IdLaboratorio;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/medicamentos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);

        if (medicamento is null)
            return NotFound();

        _context.Medicamentos.Remove(medicamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
