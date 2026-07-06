using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Pedido
{
    public int id { get; set; }

    public int cliente_id { get; set; }

    public int? promotor_id { get; set; }

    public DateTime fecha { get; set; }

    public string estado { get; set; } = null!;

    public decimal total { get; set; }

    public virtual Usuario cliente { get; set; } = null!;

    public virtual ICollection<DetallePedido> detalle_pedidos { get; set; } = new List<DetallePedido>();

    public virtual Usuario? promotor { get; set; }
}
