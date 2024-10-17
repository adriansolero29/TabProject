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

        protected async Task<Guid?> ExecuteAsync(ObjectLoader? entity, IDbTransaction? transaction = null)
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
                    NpgsqlConnection? databaseConnection = Connection.DbConnection.ConnectionInstance;
                    string json = "";
                    if (databaseConnection != null)
                    {
                        var result = transaction != null ? await databaseConnection.ExecuteScalarAsync(sql, param, Connection.DbConnection.Transaction) : await databaseConnection.ExecuteScalarAsync(sql, param);
                        json = JsonConvert.SerializeObject(result, Formatting.Indented);
                    }

                    return json;
                }, sql);

                var newToGuid = json.Replace(@"""", string.Empty);
                var jsonGuid = new Guid(newToGuid);
                return jsonGuid;
            }
            catch (Exception ex)
            {
                Connection.DbConnection.Transaction?.Rollback();
                throw;
            }
        }

        protected void BeginTransaction()
        {
            NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=AMCSentinel333!;Host=localhost;Port=5432;Database=TabulationDB;");
            Connection.DbConnection.ConnectionInstance = connection;
            Connection.DbConnection.ConnectionInstance.Open();
            Connection.DbConnection.Transaction = Connection.DbConnection.ConnectionInstance.BeginTransaction();
        }

        protected void CommitTransaction()
        {
            if (Connection.DbConnection.Transaction != null)
            {
                Connection.DbConnection.Transaction.Commit();
            }
        }

        protected void RollbackTransaction()
        {
            if (Connection.DbConnection.Transaction != null)
            {
                Connection.DbConnection.Transaction.Rollback();
            }
        }

        protected void CloseConnections()
        {
            if (Connection.DbConnection.ConnectionInstance != null && Connection.DbConnection.Transaction != null)
            {
                Connection.DbConnection.ConnectionInstance.Dispose();
                Connection.DbConnection.Transaction.Dispose();
            }
        }

    }
}

