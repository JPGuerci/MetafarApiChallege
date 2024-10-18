using System.ComponentModel.DataAnnotations;

namespace MetafarApiChallege.Infrastructure.Dtos
{
    public class TransferRequest
    {
        public int CardNumberDestiny { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
    }
}
