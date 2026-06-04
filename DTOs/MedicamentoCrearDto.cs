using System;

namespace CuidarPlusAPI.DTOs;

public class MedicamentoCrearDto
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? Presentacion { get; set; }
    public int? IdLaboratorio { get; set; }
}