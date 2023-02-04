namespace SobaFw.Server;

public class BaseController<TEntity> : ControllerBase where TEntity : BaseEntity
{
    private readonly IBaseUnitOfWork<TEntity> _unitOfWork;
    public BaseController(IBaseUnitOfWork<TEntity> unitOfWork) => _unitOfWork = unitOfWork;

    [HttpPost]
    public virtual async Task<TEntity> Post(TEntity entity) => await _unitOfWork.Create(entity);

    [HttpPost("Bulk")]
    public virtual async Task<IEnumerable<TEntity>> Post(IEnumerable<TEntity> entities) => await _unitOfWork.Create(entities);

    [HttpGet]
    public virtual async Task<IEnumerable<TEntity>> Get() => await _unitOfWork.Read();

    [HttpGet("id")]
    public virtual async Task<TEntity> Get(Guid id) => await _unitOfWork.Read(id);

    [HttpPut]
    public virtual async Task<TEntity> Put(TEntity entity) => await _unitOfWork.Update(entity);

    [HttpPut("/UpdateBulk")]
    public virtual async Task Put(IEnumerable<TEntity> entities) => await _unitOfWork.Update(entities);

    [HttpDelete("{id}")]
    public virtual async Task<TEntity> Delete(Guid id) => await _unitOfWork.Delete(id);

}