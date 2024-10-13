using BaseDataAccess.EventRepository.Interface;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices.Event
{
    internal class CriteriaService : ICriteriaService
    {
        private readonly ICriteriaRepository criteriaRepository;

        public CriteriaService(ICriteriaRepository criteriaRepository)
        {
            this.criteriaRepository = criteriaRepository;
        }

        public async Task<Criteria?> Create(Criteria? criteria, IDbTransaction? transaction = null)
        {
            return await criteriaRepository.CustomExecuteTransactionalAsync(criteria, transaction);
        }

        public async Task Delete(Criteria? criteria, IDbTransaction? transaction = null)
        {
            var obj = Helpers.ObjectHelper<Criteria>.CloneObject(criteria);
            obj.IsDeleted = true;
            await criteriaRepository.ExecuteTransactionalAsync(obj, transaction);
        }

        public async Task<IEnumerable<Criteria?>> GetAll(string? condition = null)
        {
            return await criteriaRepository.GetAllAsync(condition) ?? new List<Criteria>();
        }

        public async Task<Criteria?> Update(Criteria? criteria, IDbTransaction? transaction = null)
        {
            var obj = Helpers.ObjectHelper<Criteria>.CloneObject(criteria);
            obj.ModifiedOn = DateTime.UtcNow;
            return await criteriaRepository.ExecuteTransactionalAsync(obj, transaction);
        }
    }
}
