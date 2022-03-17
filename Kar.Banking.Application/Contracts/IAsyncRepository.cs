namespace Kar.Banking.Application.Contracts;
public interface IAsyncRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
}
