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

namespace BaseDataAccess.EventRepository.Repository
{
    public class CriteriaRepository : DataManipulator<Criteria, CriteriaDTO>, ICriteriaRepository
    {
        public Task<Criteria?> CustomExecuteAsync(Criteria? entity = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Criteria?> CustomExecuteTransactionalAsync(Criteria? entity = null, IDbTransaction? transaction = null)
        {
            var resId = await base.ExecuteAsync(entity, transaction);
            var result = ObjectHelper<Criteria>.CloneObject(entity);
            result.Id = resId;
            return result;
        }

        public async Task<Criteria?> ExecuteTransactionalAsync(Criteria? entity, IDbTransaction? transaction)
        {
            var resId = await base.ExecuteAsync(entity, transaction);
            var result = ObjectHelper<Criteria>.CloneObject(entity);
            result.Id = resId;
            return result;
        }

        public async Task<IEnumerable<Criteria>?> GetAllAsync(string? condition = null)
        {
            return await base.RetrieveAsync(condition);
        }

        public async Task<Criteria?> GetByIdAsync(Guid? id, string? condition = null)
        {
            var result = await base.RetrieveAsync(@"WHERE res.""MainObject""->>'Id' = '" + id + "'");
            return result.FirstOrDefault();
        }
    }
}
