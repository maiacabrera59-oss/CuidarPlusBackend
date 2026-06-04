using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuidarPlusAPI.Models;
using CuidarPlusAPI.DTOs;
using CuidarPlusAPI.Data;

namespace CuidarPlusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly CuidarPlusContext _context;

    public UsuarioController(CuidarPlusContext context)
    {
        _context = context;
    }

    // GET: api/Usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
    {
        var usuarios = await _context.Usuarios
            .Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Ciudad = u.Ciudad,
                FechaNacimiento = u.FechaNacimiento,
                Dni = u.Dni,
                Foto = u.Foto,
                FechaAlta = u.FechaAlta,
                FechaBaja = u.FechaBaja,
                Mail = u.Mail,
                IdGrupoSanguineo = u.IdGrupoSanguineo,
                IdUsuarioTipo = u.IdUsuarioTipo,
                IdAlergia = u.IdAlergia,
                IdCondicion = u.IdCondicion,
                IdSeguroMedico = u.IdSeguroMedico,
                IdUsuarioPadre = u.IdUsuarioPadre,
                IdParentezco = u.IdParentezco
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    // GET: api/Usuario/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto>> GetById(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario is null)
            return NotFound();

        var dto = new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Ciudad = usuario.Ciudad,
            FechaNacimiento = usuario.FechaNacimiento,
            Dni = usuario.Dni,
            Foto = usuario.Foto,
            FechaAlta = usuario.FechaAlta,
            FechaBaja = usuario.FechaBaja,
            Mail = usuario.Mail,
            IdGrupoSanguineo = usuario.IdGrupoSanguineo,
            IdUsuarioTipo = usuario.IdUsuarioTipo,
            IdAlergia = usuario.IdAlergia,
            IdCondicion = usuario.IdCondicion,
            IdSeguroMedico = usuario.IdSeguroMedico,
            IdUsuarioPadre = usuario.IdUsuarioPadre,
            IdParentezco = usuario.IdParentezco
        };

        return Ok(dto);
    }

    // POST: api/Usuario
    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Create([FromBody] UsuarioCrearDto crearDto)
    {
        var usuario = new Usuario
        {
            Nombre = crearDto.Nombre,
            Apellido = crearDto.Apellido,
            Ciudad = crearDto.Ciudad,
            FechaNacimiento = crearDto.FechaNacimiento,
            Dni = crearDto.Dni,
            Foto = crearDto.Foto,
            FechaAlta = crearDto.FechaAlta,
            FechaBaja = crearDto.FechaBaja,
            Mail = crearDto.Mail,
            IdGrupoSanguineo = crearDto.IdGrupoSanguineo,
            IdUsuarioTipo = crearDto.IdUsuarioTipo,
            IdAlergia = crearDto.IdAlergia,
            IdCondicion = crearDto.IdCondicion,
            IdSeguroMedico = crearDto.IdSeguroMedico,
            IdUsuarioPadre = crearDto.IdUsuarioPadre,
            IdParentezco = crearDto.IdParentezco
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var dto = new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Ciudad = usuario.Ciudad,
            FechaNacimiento = usuario.FechaNacimiento,
            Dni = usuario.Dni,
            Foto = usuario.Foto,
            FechaAlta = usuario.FechaAlta,
            FechaBaja = usuario.FechaBaja,
            Mail = usuario.Mail,
            IdGrupoSanguineo = usuario.IdGrupoSanguineo,
            IdUsuarioTipo = usuario.IdUsuarioTipo,
            IdAlergia = usuario.IdAlergia,
            IdCondicion = usuario.IdCondicion,
            IdSeguroMedico = usuario.IdSeguroMedico,
            IdUsuarioPadre = usuario.IdUsuarioPadre,
            IdParentezco = usuario.IdParentezco
        };

        return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, dto);
    }

    // PUT: api/Usuario/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioActualizarDto actualizarDto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario is null)
            return NotFound();

        usuario.Nombre = actualizarDto.Nombre;
        usuario.Apellido = actualizarDto.Apellido;
        usuario.Ciudad = actualizarDto.Ciudad;
        usuario.FechaNacimiento = actualizarDto.FechaNacimiento;
        usuario.Dni = actualizarDto.Dni;
        usuario.Foto = actualizarDto.Foto;
        usuario.FechaAlta = actualizarDto.FechaAlta;
        usuario.FechaBaja = actualizarDto.FechaBaja;
        usuario.Mail = actualizarDto.Mail;
        usuario.IdGrupoSanguineo = actualizarDto.IdGrupoSanguineo;
        usuario.IdUsuarioTipo = actualizarDto.IdUsuarioTipo;
        usuario.IdAlergia = actualizarDto.IdAlergia;
        usuario.IdCondicion = actualizarDto.IdCondicion;
        usuario.IdSeguroMedico = actualizarDto.IdSeguroMedico;
        usuario.IdUsuarioPadre = actualizarDto.IdUsuarioPadre;
        usuario.IdParentezco = actualizarDto.IdParentezco;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Usuario/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario is null)
            return NotFound();

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}