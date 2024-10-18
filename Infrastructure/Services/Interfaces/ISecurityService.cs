using MetafarApiChallege.Infrastructure.Dtos;

namespace MetafarApiChallege.Infrastructure.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<AuthenticateResponse> Authenticate(AccountRequest account);
    }
}
