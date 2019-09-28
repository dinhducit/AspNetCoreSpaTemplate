using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreSPATemplate.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
