using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetafarApiChallege.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ChallengeContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ChallengeContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task Add(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public async Task<T> GetById(Guid id)
        {
            var aux= await _dbSet.FirstOrDefaultAsync();
            var Item = await _dbSet.FindAsync(id);
            return Item!;
        }

    }
}
