namespace MetafarApiChallege.Infrastructure.Repositories.Models;

public partial class OperationType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
