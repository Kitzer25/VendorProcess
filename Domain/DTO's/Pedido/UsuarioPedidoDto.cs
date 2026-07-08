namespace Domain.DTO_s.Pedido;

public class UsuarioPedidoDto
{
    public string nombre { get; set; } = null!;
    
    public string email { get; set; } = null!;
    
    public string? telefono { get; set; }
    
    public string? farmacia { get; set; }
}