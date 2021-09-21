using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class ResponseResult<T> where T : class
    {
        public List<string> Errors { get; set; }
        public T Result { get; set; }
        public ResponseResult()
        {
            Errors = new List<string>();
        }            
    }
}
