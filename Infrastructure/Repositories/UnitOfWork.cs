using MetafarApiChallege.Infrastructure.Repositories.Interfaces;

namespace MetafarApiChallege.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private ChallengeContext _context;

        public UnitOfWork(ChallengeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int Commit() => _context.SaveChanges();

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
                _context = null!;
            }
        }
    }
}
