using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class RefreshToken
{
    public long id { get; set; }

    public int usuario_id { get; set; }

    public string token_hash { get; set; } = null!;

    public string? dispositivo { get; set; }

    public string? ip_address { get; set; }

    public string? user_agent { get; set; }

    public DateTime expiracion { get; set; }

    public bool revocado { get; set; }

    public DateTime creado_en { get; set; }

    public virtual Usuario usuario { get; set; } = null!;
}
