namespace Eshop.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();
        TEntity Insert(TEntity entity);
        TEntity? FindById(uint id);
    }
}
