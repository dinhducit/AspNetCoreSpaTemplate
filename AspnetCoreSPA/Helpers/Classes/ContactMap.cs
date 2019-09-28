using AspnetCoreSPATemplate.Domain.Models;
using CsvHelper.Configuration;

namespace AspnetCoreSPATemplate.Helpers.Classes
{
    public sealed class ContactMap : CsvClassMap<Contact>
    {
        public ContactMap()
        {
            Map(m => m.FirstName).Name("first_name");
            Map(m => m.LastName).Name("last_name");
            Map(m => m.CompanyName).Name("company_name");
            Map(m => m.Address).Name("address");
            Map(m => m.City).Name("city");
            Map(m => m.State).Name("state");
            Map(m => m.Post).Name("post");
            Map(m => m.PhoneNumber1).Name("phone1");
            Map(m => m.PhoneNumber2).Name("phone2");
            Map(m => m.Email).Name("email");
            Map(m => m.WebAddress).Name("web");
        }
    }

}
