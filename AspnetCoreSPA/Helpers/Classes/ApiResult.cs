using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Helpers.Classes
{
    public class ApiResult<TEntity> where TEntity : class
    {
        public int Count { get; set; }
        public IEnumerable<TEntity> List { get; set; }
    }
}
