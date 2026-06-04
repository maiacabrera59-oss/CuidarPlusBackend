using Microsoft.EntityFrameworkCore;
using CuidarPlusAPI.Models;

namespace CuidarPlusAPI.Data;

public class CuidarPlusContext : DbContext
{
    public CuidarPlusContext(DbContextOptions<CuidarPlusContext> options) : base(options) { }

    public DbSet<GrupoSanguineo> GrupoSanguineos { get; set; }
    public DbSet<Alergia> Alergias { get; set; }
    public DbSet<Condicion> Condicions { get; set; }
    public DbSet<SeguroMedico> SeguroMedicos { get; set; }
    public DbSet<UsuarioTipo> UsuarioTipos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Telefono> Telefonos { get; set; }
    public DbSet<Laboratorio> Laboratorios { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<Componente> Componentes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Especialidad> Especialidads { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<HistorialAnimo> HistorialAnimos { get; set; }
    public DbSet<Recordatorio> Recordatorios { get; set; }
    public DbSet<Horario> Horarios { get; set; }
    public DbSet<Notificacion> Notificacions { get; set; }
    public DbSet<Receta> Recetas { get; set; }
    public DbSet<RegistroToma> RegistroTomas { get; set; }
    public DbSet<Tratamiento> Tratamientos { get; set; }
}
