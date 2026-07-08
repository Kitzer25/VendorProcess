using Domain.DTO_s.Users;
using Domain.Entities;

namespace Domain.Ports.Repositories.ERepositories;

public interface IUsuarioRepository :
    IGRepositories<Usuario>
{
    Task<bool> EmailExist(string email, CancellationToken ct);
    Task<UserInfoDto?> GetByPharamacie(string pharma, CancellationToken ct);
    Task<IEnumerable<UserOrderDto>> GetUserOrdersQunatity(CancellationToken ct);
}
