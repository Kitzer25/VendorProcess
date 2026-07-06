using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DescuentosProducto
{
    public int id { get; set; }

    public int producto_id { get; set; }

    public decimal cantidad_minima { get; set; }

    public decimal precio_unitario { get; set; }

    public virtual Producto producto { get; set; } = null!;
}
