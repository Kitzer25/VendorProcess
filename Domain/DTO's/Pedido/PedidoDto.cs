namespace Domain.DTO_s.Pedido;

public class PedidoDto
{
    public int id { get; set; }

    public int cliente_id { get; set; }

    public int? promotor_id { get; set; }

    public DateTime fecha { get; set; }

    public string estado { get; set; } = null!;

    public decimal total { get; set; }
    
    public UsuarioPedidoDto cliente { get; set; }
}