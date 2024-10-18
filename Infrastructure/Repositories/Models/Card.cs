namespace MetafarApiChallege.Infrastructure.Repositories.Models;

public partial class Card
{
    public Guid Id { get; set; }

    public Guid IdAccount { get; set; }

    public int CardNumber { get; set; }

    public int Pin { get; set; }

    public int? LoginFailures { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
