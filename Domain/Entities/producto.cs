using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Producto
{
    public int id { get; set; }

    public string codigo { get; set; } = null!;

    public string descripcion { get; set; } = null!;

    public string? medida { get; set; }

    public decimal stock { get; set; }

    public decimal precio_venta { get; set; }

    public int laboratorio_id { get; set; }

    public bool activo { get; set; }

    public DateTime actualizado_en { get; set; }

    public virtual ICollection<DescuentosProducto> descuentos_productos { get; set; } = new List<DescuentosProducto>();

    public virtual ICollection<DetallePedido> detalle_pedidos { get; set; } = new List<DetallePedido>();

    public virtual Laboratorio laboratorio { get; set; } = null!;
}
