namespace Domain.DTO_s.Users;

public class UserInfoDto
{
    public string nombre { get; set; } = null!;
    
    public string? direccion { get; set; }

    public string? farmacia { get; set; }

    public string? telefono { get; set; }
}