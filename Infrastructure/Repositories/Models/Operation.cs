namespace MetafarApiChallege.Infrastructure.Repositories.Models;

public partial class Operation
{
    public Guid Id { get; set; }

    public Guid IdAccount { get; set; }

    public Guid IdOperationType { get; set; }

    public Guid IdExecutingCard { get; set; }

    public DateTime Date { get; set; }

    public decimal? Amount { get; set; }

    public decimal? InitialAmount { get; set; }

    public decimal? FinalAmount { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Card ExecutingCard { get; set; } = null!;

    public virtual OperationType OperationType { get; set; } = null!;
}
