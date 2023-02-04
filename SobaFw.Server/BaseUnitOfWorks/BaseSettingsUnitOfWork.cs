namespace SobaFw.Server;

public class BaseSettingsUnitOfWork<TEntity> : BaseUnitOfWork<TEntity>, IBaseSettingsUnitOfWork<TEntity>
    where TEntity : BaseSettingsEntity
{
    private readonly IBaseSettingsRepository<TEntity> _baseSettingsRepository;

    public BaseSettingsUnitOfWork(IBaseSettingsRepository<TEntity> baseSettingsRepository, IValidator<TEntity> validator) : base(baseSettingsRepository, validator) => _baseSettingsRepository = baseSettingsRepository;
    public async Task<IEnumerable<TEntity>> Search(string searchText) => await _baseSettingsRepository.Search(searchText);
}