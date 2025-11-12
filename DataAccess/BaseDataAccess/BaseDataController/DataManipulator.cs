using Base.PropertyBase;
using Dapper;
using ObjectLoader.Candidate;
using Newtonsoft.Json;
using Serilog;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using ObjectLoader.Event;
using System.Runtime.Serialization;

namespace BaseDataAccess.BaseDataController
{
    public abstract class DataManipulator<ObjectLoader, ModelDTO>
        where ObjectLoader : ModelBase
        where ModelDTO : DTOModel
    {
        private class Root
        {
            [JsonProperty("MainObject")]
            public ObjectLoader? MainObject { get; set; }
        }
        protected async Task ExecuteHardDelete(Guid? objId)
        {
            try
            {
                ObjectLoader instance = Activator.CreateInstance<ObjectLoader>();
                string deleteSql = instance.SqlHardDelete;

                var json = await Connection.DbConnection.StartConnection(async () =>
                {
                    NpgsqlConnection? databaseConnection = Connection.DbConnection.ConnectionInstance;
                    if (databaseConnection != null)
                    {
                        await databaseConnection.ExecuteAsync(deleteSql);
                    }

                    return string.Empty;
                }, deleteSql);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected async Task<IEnumerable<ObjectLoader>?> RetrieveAsync(string? condition = null)
        {
            try
            {
                IEnumerable<Root>? output = null;
                IEnumerable<ObjectLoader>? result = null;
                ObjectLoader instance = Activator.CreateInstance<ObjectLoader>();
                string sql = instance.SqL + " " + condition + " ";
                BeginTransaction();
                var json = await Connection.DbConnection.StartConnection(async () =>
                {
                    NpgsqlConnection? databaseConnection = Connection.DbConnection.ConnectionInstance;
                    string json = "";
                    if (databaseConnection != null)
                    {
                        json = await databaseConnection.QueryFirstAsync<string>(sql);
                    }

                    return json;
                }, sql);

                Log.Information("Retrieving Data for model: " + typeof(ObjectLoader).FullName);
                Log.Debug(json);

                if (!string.IsNullOrEmpty(json))
                {
                    output = JsonConvert.DeserializeObject<IEnumerable<Root>>(json);
                    result = (IEnumerable<ObjectLoader>?)output?.Select(a => a.MainObject) ?? new List<ObjectLoader>();
                }
                CloseConnections();
                return result ?? new List<ObjectLoader>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected async Task<Guid?> ExecuteQueryAsync(ObjectLoader? entity, IDbConnection? connection, IDbTransaction? transaction)
        {
            try
            {
                ObjectLoader instance = Activator.CreateInstance<ObjectLoader>();
                ModelDTO dtoInstance = Activator.CreateInstance<ModelDTO>();
                var param = new DynamicParameters();

                var dtoProperties = dtoInstance.ToDto(entity);

                var properties = dtoProperties?.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public);
                var idCheck = entity?.GetType().GetProperty("Id").GetValue(entity);

                var sql = idCheck == null ? instance.SqlInsert : instance.SqlUpdate;

                if (properties != null)
                    foreach (var property in properties)
                    {
                        var getMethod = property.GetGetMethod(false);
                        // check if current property is not override from base class
                        if (getMethod.GetBaseDefinition() == getMethod)
                            param.Add("@" + property.Name, property.GetValue(dtoProperties));
                    }

                var json = await Connection.DbConnection.StartConnection(async () =>
                {
                    string json = "";

                    if (connection != null && transaction != null)
                    {
                        var result = await connection.ExecuteScalarAsync(sql, param, transaction);
                        json = JsonConvert.SerializeObject(result, Formatting.Indented);
                    }
                    else
                        throw new ArgumentNullException("CONNECTION OR TRANSACTION IS NULL");

                    return json;
                }, sql);


                var newToGuid = idCheck != null ? idCheck : json.Replace(@"""", string.Empty);

                if (newToGuid.ToString().ToUpper() == "NULL")
                {
                    throw new ArgumentNullException("Returned ID is null");
                }

                var jsonGuid = new Guid(newToGuid.ToString());
                return jsonGuid;
            }
            catch (Exception ex)
            {
                Connection.DbConnection.Transaction?.Rollback();
                throw;
            }
        }

        public static IDbTransaction BeginTransaction()
        {
            Connection.DbConnection.Transaction = Connection.DbConnection.ConnectionInstance?.BeginTransaction();
            return Connection.DbConnection.Transaction ?? throw new ArgumentNullException("Transaction is null");
        }

        public static IDbConnection OpenConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=AMCSentinel333!;Host=localhost;Port=5432;Database=TabulationDB;");
            Connection.DbConnection.ConnectionInstance = connection;
            Connection.DbConnection.ConnectionInstance.Open();

            return Connection.DbConnection.ConnectionInstance;
        }

        public static void CommitTransaction()
        {
            if (Connection.DbConnection.Transaction != null)
            {
                Connection.DbConnection.Transaction.Commit();
            }
        }

        public static void RollbackTransaction()
        {
            if (Connection.DbConnection.Transaction != null)
            {
                Connection.DbConnection.Transaction.Rollback();
            }
        }

        public static void CloseConnections()
        {
            if (Connection.DbConnection.ConnectionInstance != null && Connection.DbConnection.Transaction != null)
            {
                Connection.DbConnection.ConnectionInstance.Dispose();
                Connection.DbConnection.Transaction.Dispose();
            }
        }
    }
}

