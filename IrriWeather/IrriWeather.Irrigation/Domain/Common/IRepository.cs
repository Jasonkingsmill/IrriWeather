using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Common
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity, TKey> : IRepository where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);

        TEntity Find(TKey id);
        IEnumerable<TEntity> FindAll();
    }
}
