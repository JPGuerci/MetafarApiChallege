namespace MetafarApiChallege.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task Add(T entity);
        void Update(T entity);
        Task <T> GetById(Guid id);
    }
}
