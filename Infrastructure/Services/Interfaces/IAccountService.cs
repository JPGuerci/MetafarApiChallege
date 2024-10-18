using MetafarApiChallege.Infrastructure.Dtos;

namespace MetafarApiChallege.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> GetByIdCard(Guid idCard);
        Task<OperationDto> CashOut(Guid idCard, decimal amount);
        Task<OperationDto> CashIn(Guid idCard, decimal amount);
        Task<TransferResponse> Transfer(TransferDto transfer);
    }
}
