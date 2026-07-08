namespace Domain.DTO_s.Pedido;

public class OrdersQuantityClientDto
{
    public string nombre { get; set; } = null!;

    public string? farmacia { get; set; }

    public int qunatity { get; set; }

}