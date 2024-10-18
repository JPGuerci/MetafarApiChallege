using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Repositories.Interfaces
{
    public interface ICardRepository : IGenericRepository<Card>
    {
        Task<Card> GetByCardNumber(int CardNumber);
    }
}
