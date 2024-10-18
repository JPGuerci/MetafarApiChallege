using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetByCardNumber(int CardNumber);
        Task<Account?> GetByIdCard(Guid IdCard);
        
    }
}
