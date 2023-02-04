namespace SobaFw.Server;

public class BaseSettingsController<TEntity> : BaseController<TEntity> where TEntity : BaseSettingsEntity
{
    private readonly IBaseSettingsUnitOfWork<TEntity> _baseSettingsUnitOfWork;

    protected BaseSettingsController(IBaseSettingsUnitOfWork<TEntity> unitOfWork) : base(unitOfWork) => _baseSettingsUnitOfWork = unitOfWork;

    [HttpGet("Search/{searchText}")]
    public virtual async Task<IEnumerable<TEntity>> Search([FromRoute] string searchText) => await _baseSettingsUnitOfWork.Search(searchText);
}