namespace Domain.DTO_s.Products;

public class ProductDto
{
    public string codigo { get; set; } = null!;

    public string descripcion { get; set; } = null!;
    
    public decimal stock { get; set; }

    public decimal precio_venta { get; set; }
}