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
using System.Xml.XPath;

namespace BaseDataAccess.EventRepository.Repository
{
    public class ContestRepository : DataManipulator<Contest, ContestDTO>, IContestRepository
    {
        public async Task<Contest?> ExecuteAsync(Contest? entity)
        {
            try
            {
                var connection = OpenConnection();
                var transaction = BeginTransaction();

                var resId = await base.ExecuteQueryAsync(entity, connection, transaction);
                var clone = ObjectHelper<Contest>.CloneObjectJson(entity);
                clone.Id = resId;
                CommitTransaction();
                CloseConnections();

                return clone;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw;
            }
        }
  
        public async Task<Contest?> ExecuteAsyncTran(Contest? entity, IDbConnection connection, IDbTransaction transaction)
        {
            try
            {
                var resId = await base.ExecuteQueryAsync(entity, connection, transaction);
                var clone = ObjectHelper<Contest>.CloneObjectJson(entity);
                clone.Id = resId;
                CommitTransaction();
                CloseConnections();

                return clone;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw;
            }
        }

        public async Task<IEnumerable<Contest>?> GetAllAsync(string? condition = null)
        {
            OpenConnection();
            return await base.RetrieveAsync(condition);
        }

        public async Task<Contest?> GetByIdAsync(Guid? id, string? condition = null)
        {
            var result = await base.RetrieveAsync(@"WHERE res.""MainObject""->>'Id' = '" + id + "'");
            return result.FirstOrDefault();
        }
    }
}
