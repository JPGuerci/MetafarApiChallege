using AutoMapper;
using MetafarApiChallege.Infrastructure.Dtos;
using MetafarApiChallege.Infrastructure.Helpers;
using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Repositories.Models;
using MetafarApiChallege.Infrastructure.Services.Interfaces;

namespace MetafarApiChallege.Infrastructure.Services
{
    public class OperationService : IOperationService
    {
        private readonly IMapper _mapper;
        private readonly IOperationRepository _operationRepository;
        private readonly IOperationTypeRepository _operationTypeRepository;

        public OperationService(IMapper mapper, IOperationRepository operationRepository, IOperationTypeRepository operationTypeRepository)
        {
            _mapper = mapper;
            _operationRepository = operationRepository;
            _operationTypeRepository = operationTypeRepository;
        }

        public async Task<OperationDto> GetLast(Guid IdAccount, string TypeTransacction)
        {
            Operation lastOperation = await _operationRepository.GetLast(IdAccount, TypeTransacction);
            OperationDto response = _mapper.Map<OperationDto>(lastOperation);

            return response;
        }

        public async Task<OperationDto> AddOperation(Account account, decimal amount, Guid idExecutingCard, string typeTransacction)
        {
            OperationType operationType = await _operationTypeRepository.GetByName(typeTransacction);
            Operation operation = new Operation()
            {
                Id = Guid.NewGuid(),
                IdAccount = account.Id,
                IdOperationType = operationType.Id,
                IdExecutingCard = idExecutingCard,
                Date = DateTime.Now,
                Amount = amount,
                InitialAmount = account.CurrentBalance,
                FinalAmount = account.CurrentBalance + amount,
            };
            await _operationRepository.Add(operation);
            // Armado de respuesta
            OperationDto response = _mapper.Map<OperationDto>(operation);
            response.AccountNumber = account.AccountNumber;
            response.OperationType = typeTransacction;

            return response;
        }

        public async Task<PagedResult<OperationDto>> GetOperations(Guid idCard, int pageNumber, int pageSize)
        {
            List<Operation> operations = await _operationRepository.GetByIdCard(idCard, pageNumber, pageSize);
            int totalCount = await _operationRepository.GetTotalCountByIdCard(idCard);

            List<OperationDto> result = _mapper.Map<List<OperationDto>>(operations);
            return new PagedResult<OperationDto>
            {
                Items = result,
                TotalCount = totalCount
            };
        }
    }
}
