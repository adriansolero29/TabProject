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

        public async Task<Criterion?> Create(Criterion? criterion)
        {
            return await criterionRepository.ExecuteAsync(criterion);
        }

        public async Task<Criterion?> CreateTran(Criterion? criterion, IDbConnection connection, IDbTransaction transaction)
        {
            return await criterionRepository.ExecuteAsyncTran(criterion, connection, transaction);
        }

        public Task Delete(Criterion? criterion, IDbTransaction? transaction = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Criterion?>> GetAll(string? condition = null)
        {
            return await criterionRepository.GetAllAsync(condition) ?? new List<Criterion>();
        }

        public async Task<IEnumerable<Criterion>?> GetByCriteria(Guid? criteriaId)
        {
            return await criterionRepository.GetByCriteria(criteriaId);
        }

        public Task<Criterion?> Update(Criterion? criterion)
        {
            throw new NotImplementedException();
        }

        public Task<Criterion?> UpdateTran(Criterion? criterion, IDbConnection connection, IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
