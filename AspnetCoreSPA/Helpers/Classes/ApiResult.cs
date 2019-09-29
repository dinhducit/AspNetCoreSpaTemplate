using System.Collections.Generic;

namespace AspnetCoreSPATemplate.Helpers.Classes
{
    public class ApiResult<TEntity> where TEntity : class
    {
        public Page Page { get; set; }
        public IEnumerable<TEntity> Result { get; set; }
    }
}
