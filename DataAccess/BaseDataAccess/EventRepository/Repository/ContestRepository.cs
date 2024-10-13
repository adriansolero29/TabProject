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
        public Task<Contest?> CustomExecuteAsync(Contest? entity = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Contest?> CustomExecuteTransactionalAsync(Contest? entity = null, IDbTransaction? transaction = null)
        {
            try
            {
                BeginTransaction();
                var resId = await base.ExecuteAsync(entity, transaction);
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

        public async Task<Contest?> ExecuteTransactionalAsync(Contest? entity, IDbTransaction? transaction)
        {
            var resId = await base.ExecuteAsync(entity, transaction);
            return null;
        }

        public async Task<IEnumerable<Contest>?> GetAllAsync(string? condition = null)
        {
            return await base.RetrieveAsync(condition);
        }

        public async Task<Contest?> GetByIdAsync(Guid? id, string? condition = null)
        {
            var result = await base.RetrieveAsync(@"WHERE res.""MainObject""->>'Id' = '" + id + "'");
            return result.FirstOrDefault();
        }
    }
}
