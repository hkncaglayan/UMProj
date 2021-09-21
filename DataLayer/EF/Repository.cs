using Common;
using DataLayer;
using DataLayer.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        private DbSet<T> _objSet;
        public Repository()
        {
            _objSet = db.Set<T>();
        }
        public List<T> List()
        {
            return _objSet.ToList();
        }
        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return _objSet.Where(expression).ToList();
        }
        public int Create(T obj)
        {
            _objSet.Add(obj);
            if (obj is BaseEntity)
            {
                var activeUser = App.Common.GetActiveUser();
                DateTime now = DateTime.Now;
                BaseEntity o = obj as BaseEntity;
                o.Created = now;
                o.CreatedBy = activeUser.Id;
                o.CreatedByName = activeUser.Name;
                o.LastUpdated = now;
                o.LastUpdatedBy = activeUser.Id;
                o.LastUpdatedByName = activeUser.Name;
            }
            return Save();
        }
        public int Update(T obj)
        {
            if (obj is BaseEntity)
            {
                var activeUser = App.Common.GetActiveUser();
                BaseEntity o = obj as BaseEntity;
                o.LastUpdated = DateTime.Now;
                o.LastUpdatedBy = activeUser.Id;
                o.LastUpdatedByName = activeUser.Name;
            }
            return Save();
        }
        public int Delete(T obj)
        {
            if (obj is BaseEntity)
            {
                var activeUser = App.Common.GetActiveUser();
                BaseEntity o = obj as BaseEntity;
                o.Deleted = DateTime.Now;
                o.DeletedBy = activeUser.Id;
                o.DeletedByName = activeUser.Name;
            }
            //_objSet.Remove(obj);
            return Save();
        }
        public T Find(Expression<Func<T, bool>> expression)
        {
            return _objSet.FirstOrDefault(expression);
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
