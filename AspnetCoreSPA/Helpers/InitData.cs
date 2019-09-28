using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Domain;
using AspnetCoreSPATemplate.Domain.Models;
using AspnetCoreSPATemplate.Helpers.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCoreSPATemplate.Helpers
{
    public static class InitData
    {
        public static async Task InitializeDatabase(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<ApplicationDbContext>();

                if (await db.Database.EnsureCreatedAsync())
                {
                    await InsertData(scopeServiceProvider);
                }
            }
        }

        private static async Task InsertData(IServiceProvider serviceProvider)
        {
            // TODO: should refactor the csv file path, using config etc...
            var currentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
            var contacts = CsvHelpers.ParseCsv<Contact, ContactMap>($"{currentDirectory}\\AspnetCoreSPA\\SampleData.csv");

            await AddOrUpdateAsync(serviceProvider, contacts);
        }

        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider
            , IEnumerable<TEntity> entities)
            where TEntity : class
        {
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                foreach (var item in entities)
                {
                    db.Entry(item).State = existingData.Contains(item)
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
