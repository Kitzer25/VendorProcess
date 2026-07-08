namespace Domain.DTO_s.Products;

public class ListProductsDto
{
    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
    
    public decimal Stock { get; set; }

    public decimal PrecioVenta { get; set; }
}