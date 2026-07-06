using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DetallePedido
{
    public int id { get; set; }

    public int pedido_id { get; set; }

    public int producto_id { get; set; }

    public decimal cantidad { get; set; }

    public decimal precio_unitario_aplicado { get; set; }

    public decimal subtotal { get; set; }

    public virtual Pedido pedido { get; set; } = null!;

    public virtual Producto producto { get; set; } = null!;
}
