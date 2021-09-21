using DataLayer;
using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    public class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _lockObj = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }
        public static void CreateContext()
        {
            if (db == null)
            {
                lock (_lockObj)
                {
                    db = new DatabaseContext();
                }
            }
        }
    }
}
