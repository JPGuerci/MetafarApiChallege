using MetafarApiChallege.Infrastructure.Dtos;
using MetafarApiChallege.Infrastructure.Helpers;
using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Services.Interfaces
{
    public interface IOperationService
    {
        Task<OperationDto> GetLast(Guid idAccount, string TypeTransacction);
        Task<OperationDto> AddOperation(Account account, decimal amount, Guid idExecutingCard, string typeTransacction);
        Task<PagedResult<OperationDto>> GetOperations(Guid idCard, int pageNumber, int pageSize);
    }
}
