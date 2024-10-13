using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace BaseDataAccess.BaseDataController.Connection
{
    public class DbConnection
    {
        public static NpgsqlConnection? ConnectionInstance { get; internal set; }
        public static IDbTransaction? Transaction { get; internal set; }

        public static async Task<string> StartConnection(Func<Task<string>> queryHandling, string query)
        {


            return await queryHandling.Invoke();
        }
    }
}
