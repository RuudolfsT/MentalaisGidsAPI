namespace ServiceLayer.Interface
{
    public interface IBaseManager<T>
    {
        Task<T> FindById(int id);
        Task<T> SaveOrUpdate(T entity);
        void Delete(T entity);
    }
}
