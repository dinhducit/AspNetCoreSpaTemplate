using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Helpers.Classes
{
    public class Paging
    {
        public string SearchColumn { get; set; }
        public string SearchValue { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderType { get; set; }
        public int TotalPage { get; set; }
    }
}
