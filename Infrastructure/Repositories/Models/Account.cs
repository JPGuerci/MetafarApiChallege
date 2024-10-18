namespace MetafarApiChallege.Infrastructure.Repositories.Models;

public partial class Account
{
    public Guid Id { get; set; }

    public int AccountNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
