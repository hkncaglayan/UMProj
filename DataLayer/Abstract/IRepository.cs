using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();
        List<T> List(Expression<Func<T, bool>> expression);
        int Create(T obj);
        int Update(T obj);
        int Delete(T obj);
        T Find(Expression<Func<T, bool>> expression);
        int Save();        
    }
}
