using Domain.DTO_s.Products;

namespace Domain.DTO_s.Laboratory;

public class LaboratoryProductsDto
{
    public string Laboratorie { get; set; } = null!;

    public IEnumerable<ProductItemDto> Products { get; set; } = new List<ProductItemDto>();

}