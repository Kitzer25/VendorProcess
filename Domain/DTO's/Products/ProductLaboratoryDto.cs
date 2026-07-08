using Domain.DTO_s.Laboratory;

namespace Domain.DTO_s.Products;

public class ProductLaboratoryDto
{
    public string laboratorie { get; set; } = null!;

    public string product { get; set; } = null!;

    public int qunatity { get; set; }
}