namespace Messenger.Core.IServices;

public interface IBaseService<T> where T : class
{
    T Insert(T entity);

    T Delete(T entity);

    T Update(T entity);
}