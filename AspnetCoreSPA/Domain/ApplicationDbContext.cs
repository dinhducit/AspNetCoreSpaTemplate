using AspnetCoreSPATemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreSPATemplate.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
