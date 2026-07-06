using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class StagingVendible
{
    public int id { get; set; }

    public string lote_carga { get; set; } = null!;

    public string? laboratorio_raw { get; set; }

    public string? codigo_raw { get; set; }

    public string? descripcion_raw { get; set; }

    public string? medida_raw { get; set; }

    public string? stock_raw { get; set; }

    public string? precio_raw { get; set; }

    public string? descuento_raw { get; set; }

    public bool procesado { get; set; }

    public string? error { get; set; }

    public DateTime cargado_en { get; set; }
}
