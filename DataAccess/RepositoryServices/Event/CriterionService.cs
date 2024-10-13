using BaseDataAccess.EventRepository.Interface;
using NpgsqlTypes;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices.Event
{
    public class CriterionService : ICriterionService
    {
        private readonly ICriterionRepository criterionRepository;

        public CriterionService(ICriterionRepository criterionRepository)
        {
            this.criterionRepository = criterionRepository;
        }

        public async Task<Criterion?> Create(Criterion? criterion, IDbTransaction? transaction = null)
        {
            return await criterionRepository.ExecuteTransactionalAsync(criterion, transaction);
        }

        public async Task Delete(Criterion? criterion, IDbTransaction? transaction = null)
        {
            var obj = Helpers.ObjectHelper<Criterion>.CloneObject(criterion);
            obj.IsDeleted = true;
            await criterionRepository.ExecuteTransactionalAsync(obj, transaction);
        }

        public async Task<IEnumerable<Criterion?>> GetAll(string? condition = null)
        {
            return await criterionRepository.GetAllAsync(condition) ?? new List<Criterion>();
        }

        public async Task<Criterion?> Update(Criterion? criterion, IDbTransaction? transaction = null)
        {
            var obj = Helpers.ObjectHelper<Criterion>.CloneObject(criterion);
            obj.ModifiedOn = DateTime.UtcNow;
            return await criterionRepository.ExecuteTransactionalAsync(obj, transaction);
        }
    }
}
