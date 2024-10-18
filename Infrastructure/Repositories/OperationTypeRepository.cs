using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Repositories.Models;

namespace MetafarApiChallege.Infrastructure.Repositories;
public class OperationTypeRepository : IOperationTypeRepository
{
    private List<OperationType> _operationTypesInMemory;

    public OperationTypeRepository()
    {
        _operationTypesInMemory = new List<OperationType>(); 
    }

    public void SetOperationTypes(List<OperationType?> operationTypes)
    {
        _operationTypesInMemory = new List<OperationType>(operationTypes);
    }

    public async Task<OperationType?> GetByName(string name)
    {
        var operationType = _operationTypesInMemory.FirstOrDefault(ot => ot.Name == name);
        return await Task.FromResult(operationType);
    }
}
