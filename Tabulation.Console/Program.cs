using System;
using BaseDataAccess.EventRepository.Repository;
using Dapper;
using Newtonsoft.Json;
using Npgsql;
using ObjectLoader.Event;

namespace Tabulation.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Load();
        }

        public async static void Load()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=AMCSentinel333!;Host=localhost;Port=5432;Database=TabulationDB;"))
            {
                connection.Open();
                string sql = @"
SELECT JSONB_AGG(res)
FROM (
    SELECT 
    JSONB_BUILD_OBJECT
    (
        'Id', crt.""Id"", 
        'ModifiedByUserId', crt.""ModifiedByUserId"", 
        'CriteriaName', crt.""CriteriaName"", 
        'Sequence', crt.""Sequence"",
        'Percentage', crt.""Percentage"", 
        'CreatedOn', crt.""CreatedOn"", 
        'ModifiedOn', crt.""ModifiedOn"",
        'Contest', JSON_BUILD_OBJECT(
            'Id', conts.""Id"", 
            'ModifiedByUserId', conts.""ModifiedByUserId"", 
            'Version', conts.""Version"", 
            'Name', conts.""Name"", 
            'CreatedOn', conts.""CreatedOn"", 
            'ModifiedOn', conts.""ModifiedOn"", 
            'DateFrom', conts.""DateFrom"", 
            'DateTo', conts.""DateTo"", 
            'Place', conts.""Place""
        )
    ) ""Criteria""
    FROM ""Event"".""Criterias"" crt
    LEFT OUTER JOIN ""Event"".""Contest"" conts ON conts.""Id"" = crt.""ContestId""
    WHERE crt.""IsDeleted"" = FALSE
) res
";

                var a = await connection.QueryFirstAsync<string>(sql);
                var test = JsonConvert.DeserializeObject<IEnumerable<Root>>(a);
            }
        }

        public class Root
        {
            public Criteria Criteria { get; set; }
        }
    }
}
