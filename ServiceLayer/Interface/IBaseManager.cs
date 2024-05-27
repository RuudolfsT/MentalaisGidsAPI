namespace ServiceLayer.Interface
{
    public interface IBaseManager<T>
    {
        Task<T> FindById(int id);
    }
}
