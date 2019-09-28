using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace AspnetCoreSPATemplate.Helpers
{
    public static class CsvHelpers
    {
        public static IEnumerable<TEntity> ParseCsv<TEntity, TEntityMap>(string csvFilePath)
            where TEntity : class
            where TEntityMap : CsvClassMap
        {
            using (var stream = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(stream)) {
                csv.Configuration.RegisterClassMap<TEntityMap>();
                return csv.GetRecords<TEntity>();
            }
        }
    }
}
