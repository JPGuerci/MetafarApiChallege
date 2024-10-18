using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace MetafarApiChallege.Infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ChallengeContext _context;

        public AccountRepository(ChallengeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account?> GetByCardNumber(int CardNumber)
        {

            Account? response = await _context.Accounts
                .Where(acc => acc.Cards.Any(c => c.CardNumber == CardNumber))
                .FirstOrDefaultAsync();

            return response;
        }
        public async Task<Account?> GetByIdCard(Guid IdCard)
        {
            Account? response = await _context.Accounts
               .Where(acc => acc.Cards.Any(c => c.Id == IdCard))
               .FirstOrDefaultAsync();

            return response;
        }

    }
}
