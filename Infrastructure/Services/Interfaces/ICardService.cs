using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Services.Interfaces
{
    public interface ICardService
    {
        Task<Card> GetByCardNumber(int cardNumber);
        Task UpdateLoginFailures(Guid idCard, int failuresCount);
    }
}
