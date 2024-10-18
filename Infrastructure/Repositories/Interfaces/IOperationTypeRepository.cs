using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Repositories.Interfaces
{
    public interface IOperationTypeRepository
    {
        Task<OperationType?> GetByName(string name);
        void SetOperationTypes(List<OperationType?> operationTypes);
    }
}
