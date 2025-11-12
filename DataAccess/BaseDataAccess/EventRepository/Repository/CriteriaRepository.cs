using BaseDataAccess.BaseDataController;
using BaseDataAccess.EventRepository.Interface;
using DTOs.Event;
using Helpers;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BaseDataAccess.EventRepository.Repository
{
    public class CriteriaRepository : DataManipulator<Criteria, CriteriaDTO>, ICriteriaRepository
    {
        public async Task<Criteria?> ExecuteAsync(Criteria? entity)
        {
            var connection = OpenConnection();
            var transaction = BeginTransaction();

            var resId = await base.ExecuteQueryAsync(entity, connection, transaction);
            var result = ObjectHelper<Criteria>.CloneObject(entity);
            result.Id = resId;

            CommitTransaction();
            CloseConnections();

            return result;
        }

        public async Task<Criteria?> ExecuteAsyncTran(Criteria? entity, IDbConnection connection, IDbTransaction transaction)
        {
            var resId = await base.ExecuteQueryAsync(entity, connection, transaction);
            var result = ObjectHelper<Criteria>.CloneObjectJson(entity);
            result.Id = resId;

            return result;
        }

        public async Task<IEnumerable<Criteria>?> GetAllAsync(string? condition = null)
        {
            OpenConnection();

            return await base.RetrieveAsync(condition);
        }

        public async Task<Criteria?> GetByIdAsync(Guid? id, string? condition = null)
        {
            OpenConnection();

            var result = await base.RetrieveAsync(@"WHERE res.""MainObject""->>'Id' = '" + id + "'");
            return result.FirstOrDefault();
        }

    }
}
