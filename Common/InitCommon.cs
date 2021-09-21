using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class InitCommon : ICommon
    {
        public User GetActiveUser()
        {
            return new User { Id = -1, Name = "SYSTEM" };
        }
    }
}
