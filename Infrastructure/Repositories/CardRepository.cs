using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace MetafarApiChallege.Infrastructure.Repositories
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        private readonly ChallengeContext _context;
        private readonly DbSet<Card> _dbSet;

        public CardRepository(ChallengeContext context) : base(context)
        {

            _context = context;

        }

        public async Task<Card> GetByCardNumber(int CardNumber)
        {
            Card result = await _context.Cards.FirstAsync(c => c.CardNumber.Equals(CardNumber));
            return result;
        }
    }
}
