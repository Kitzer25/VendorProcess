using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Laboratorio
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<Producto> productos { get; set; } = new List<Producto>();
}
