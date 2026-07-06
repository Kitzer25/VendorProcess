using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Usuario
{
    public int id { get; set; }

    public string rol { get; set; } = null!;

    public string nombre { get; set; } = null!;

    public string email { get; set; } = null!;

    public string password_hash { get; set; } = null!;

    public string? direccion { get; set; }

    public string? farmacia { get; set; }

    public string? telefono { get; set; }

    public bool activo { get; set; }

    public DateTime creado_en { get; set; }

    public virtual ICollection<Pedido> pedidoclientes { get; set; } = new List<Pedido>();

    public virtual ICollection<Pedido> pedidopromotors { get; set; } = new List<Pedido>();

    public virtual ICollection<RefreshToken> refresh_tokens { get; set; } = new List<RefreshToken>();
}
