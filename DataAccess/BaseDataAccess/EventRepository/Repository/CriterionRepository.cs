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
    public class CriterionRepository : DataManipulator<Criterion, CriterionDTO>, ICriterionRepository
    {
        public Task<Criterion?> CustomExecuteAsync(Criterion? entity = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Criterion?> CustomExecuteTransactionalAsync(Criterion? entity = null, IDbTransaction? transaction = null)
        {
            var resId = await base.ExecuteAsync(entity, transaction);
            var result = ObjectHelper<Criterion>.CloneObject(entity);
            result.Id = resId;
            return result;
        }

        public async Task<Criterion?> ExecuteTransactionalAsync(Criterion? entity, IDbTransaction? transaction)
        {
            var resId = await base.ExecuteAsync(entity, transaction);
            var result = ObjectHelper<Criterion>.CloneObject(entity);
            result.Id = resId;
            return result;
        }

        public async Task<IEnumerable<Criterion>?> GetAllAsync(string? condition = null)
        {
            return await base.RetrieveAsync(condition);
        }

        public async Task<Criterion?> GetByIdAsync(Guid? id, string? condition = null)
        {
            var result = await base.RetrieveAsync(@"WHERE res.""MainObject""->>'Id' = '" + id + "'");
            return result.FirstOrDefault();
        }
    }
}
