namespace MetafarApiChallege.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
