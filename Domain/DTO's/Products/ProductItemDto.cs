namespace Domain.DTO_s.Products;

public class ProductItemDto
{
    public string codigo { get; set; } = null!;

    public string descripcion { get; set; } = null!;

    public string? medida { get; set; }

    public decimal stock { get; set; }

    public decimal precio_venta { get; set; }
}