using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace MetafarApiChallege.Infrastructure.Repositories
{
    public class OperationRepository : GenericRepository<Operation>, IOperationRepository
    {
        private readonly ChallengeContext _context;

        public OperationRepository(ChallengeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Operation>> GetByIdCard(Guid idCard, int pageNumber, int pageSize)
        {
            return await _context.Operations
                .Include(op => op.OperationType)
                .Include(op => op.ExecutingCard)
                .Include(op => op.Account)
                .Where(op =>
                    (op.IdExecutingCard == idCard && op.Account.Cards.Any(c => c.Id == idCard)) ||
                    (op.Account.Cards.Any(c => c.Id == idCard) && op.IdExecutingCard != idCard)
                )
                .OrderByDescending(op => op.Date)
                .OrderByDescending(c => c.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        }

        public async Task<int> GetTotalCountByIdCard(Guid idCard)
        {
            return await _context.Operations
               .Include(op => op.Account)
                .Where(op =>
                    (op.IdExecutingCard == idCard && op.Account.Cards.Any(c => c.Id == idCard)) ||
                    (op.Account.Cards.Any(c => c.Id == idCard) && op.IdExecutingCard != idCard)
                )
                .CountAsync();
        }

        public async Task<Operation?> GetLast(Guid idAccount, string typeTransaction)
        {
            return await _context.Operations
                .Include(op => op.ExecutingCard)
                .Include(op => op.Account)
                .Where(op => op.IdAccount == idAccount && op.OperationType.Name.Equals(typeTransaction))
                .OrderByDescending(op => op.Date)
                .FirstOrDefaultAsync();
        }
    }
}
