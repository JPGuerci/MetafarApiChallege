namespace MetafarApiChallege.Infrastructure.Dtos
{
    public class AccountDto
    {
        public int AccountNumber { get; set; }
        public string UserName { get; set; }
        public Decimal Balance { get; set; }
        public DateTime? LastExtraction { get; set; }
    }
}
