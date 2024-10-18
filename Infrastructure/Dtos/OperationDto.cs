namespace MetafarApiChallege.Infrastructure.Dtos
{
    public class OperationDto
    {
        public int OperationNumber { get; set; }
        public DateTime Date { get; set; }
        public int AccountNumber { get; set; }
        public string? OperationType { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
