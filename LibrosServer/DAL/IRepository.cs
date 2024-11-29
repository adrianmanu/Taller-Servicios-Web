using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository : IDisposable
    {
        TEntity Create<TEntity>(TEntity toCreate) where TEntity : class;
        bool Update<TEntity>(TEntity toUpdate) where TEntity : class;
        bool Delete<TEntity>(TEntity toDelete) where TEntity : class;
        //Buscar devuelve un registro
        TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) 
            where TEntity : class;
        //Listar devuelve una lista de registros
        List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria)
            where TEntity : class;
    }
}
