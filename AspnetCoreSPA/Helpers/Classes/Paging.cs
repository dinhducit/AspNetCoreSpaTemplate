using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Helpers.Classes
{
    public class Paging
    {
        [FromQuery]
        public int Page { get; set; }
        [FromQuery]
        public int Size { get; set; }
        [FromQuery]
        public string Sort { get; set; }
        [FromQuery]
        public string Pattern { get; set; }
    }
}
