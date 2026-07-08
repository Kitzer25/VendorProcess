namespace Domain.DTO_s.Users;

public class UserOrderDto
{
    public string nombre { get; set; } = null!;
    public string? farmacia { get; set; }
    public int orderqunatity { get; set; }
}