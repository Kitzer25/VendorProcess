namespace Domain.DTO_s.Products;

public class MeasureDto
{
    public string? Medida { get; set; }

    public ProductDto? Product { get; set; } = new ProductDto();

}