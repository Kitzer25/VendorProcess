namespace Domain.DTO_s.DetallePedido;

public class OrderDetailDto
{
    public int id { get; set; }

    public int pedido_id { get; set; }

    public int producto_id { get; set; }

    public decimal cantidad { get; set; }
    
    public decimal precio_unitario_aplicado { get; set; }
    
    public decimal subtotal { get; set; }
}