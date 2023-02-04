namespace SobaFw.Client;

public interface IHttpService<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> DeleteAsync(string url);
    Task<IEnumerable<TEntity>> GetAsync(string url);
    Task<TEntity> GetByIdAsync(string url);
    Task<TEntity> PostAsync(string url, TEntity viewModel);
    Task<TEntity> PutAsync(string url, TEntity viewModel);
}
