namespace Domain.DTO_s.Users;

public class UserDto
{
    public int id { get; set; }

    public string nombre { get; set; } = null!;

    public string email { get; set; } = null!;
    
    public string? direccion { get; set; }

    public string? farmacia { get; set; }

    public string? telefono { get; set; }
}