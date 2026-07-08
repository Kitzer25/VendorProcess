namespace Domain.DTO_s.DetallePedido;

public class ProductRequiredDto
{
    public int producto_id { get; set; }
    public string producto { get; set; }
    public decimal cantidad { get; set; }
    public decimal subtotalgenerado { get; set; }
}