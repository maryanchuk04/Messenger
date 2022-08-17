using AutoMapper;
using Messenger.Core.IServices;
using Messenger.db.EF;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Core.Services;

public class BaseService<T> : IBaseService<T>
    where T : class
{
    public BaseService(MessengerContext context, IMapper mapper = null)
    {
        Context = context;
        Mapper = mapper;
    }

    protected IMapper Mapper { get; set; }
    protected MessengerContext Context { get; set; }

    protected DbSet<T> Entities { get => Context.Set<T>(); }

    public T Delete(T entity)
    {
        if (entity == null)
        {
            throw new NotImplementedException();
        }

        Entities.Remove(entity);
        return entity;
    }

    public T Insert(T entity)
    {
        if (entity == null)
        {
            throw new NotImplementedException();
        }

        Entities.Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        if (entity == null)
        {
            throw new NotImplementedException();
        }

        Entities.Update(entity);
        return entity;
    }
}