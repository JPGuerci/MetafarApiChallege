using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Repositories.Interfaces
{
    public interface IOperationRepository : IGenericRepository<Operation>
    {
        Task<Operation> GetLast(Guid IdAccount, string TypeTransacction);
        Task<List<Operation>> GetByIdCard(Guid idCard, int pageNumber, int pageSize);
        Task<int> GetTotalCountByIdCard(Guid idCard);
    }
}
