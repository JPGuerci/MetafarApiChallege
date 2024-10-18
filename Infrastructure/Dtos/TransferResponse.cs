namespace MetafarApiChallege.Infrastructure.Dtos
{
    public class TransferResponse: OperationDto
    {
        public int CardNumberDestiny { get; set; }
        public decimal Balance { get; set; }
    }
}
