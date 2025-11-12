using BaseDataAccess.BaseDataController;
using BaseDataAccess.EventRepository.Interface;
using DTOs.Event;
using Helpers;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BaseDataAccess.EventRepository.Repository
{
    public class CriterionRepository : DataManipulator<Criterion, CriterionDTO>, ICriterionRepository
    {
        public async Task<Criterion?> ExecuteAsync(Criterion? entity)
        {
            var connection = OpenConnection();
            var transaction = BeginTransaction();

            var resId = await base.ExecuteQueryAsync(entity, connection, transaction);
            var result = ObjectHelper<Criterion>.CloneObject(entity);
            result.Id = resId;

            CommitTransaction();
            CloseConnections();

            return result;
        }


        public async Task<Criterion?> ExecuteAsyncTran(Criterion? entity, IDbConnection connection, IDbTransaction transaction)
        {
            var resId = await base.ExecuteQueryAsync(entity, connection, transaction);
            var result = ObjectHelper<Criterion>.CloneObjectJson(entity);
            result.Id = resId;

            return result;
        }

        public async Task<IEnumerable<Criterion>?> GetAllAsync(string? condition = null)
        {
            OpenConnection();

            return await base.RetrieveAsync(condition);
        }

        public async Task<IEnumerable<Criterion>?> GetByCriteria(Guid? criteriaId)
        {
            OpenConnection();

            var result = await base.RetrieveAsync(@"WHERE res.""MainObject""->'Criteria'->>'Id' = '" + criteriaId + "'");
            return result ?? new List<Criterion>();
        }

        public async Task<Criterion?> GetByIdAsync(Guid? id, string? condition = null)
        {
            OpenConnection();

            var result = await base.RetrieveAsync(@"WHERE res.""MainObject""->>'Id' = '" + id + "'");
            return result.FirstOrDefault();
        }

    }
}
