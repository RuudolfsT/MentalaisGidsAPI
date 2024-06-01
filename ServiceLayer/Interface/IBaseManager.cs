namespace ServiceLayer.Interface
{
    public interface IBaseManager<T>
    {
        Task<T> FindById(int id);
        Task<T> FindByNameId(string id);
        Task<T> SaveOrUpdate(T entity);
        Task Delete(T entity);
    }
}
