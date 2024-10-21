using BaseDataAccess.EventRepository.Interface;
using ObjectLoader.Event;
using RepositoryServices.CustomModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices.Event
{
    public class CriteriaService : ICriteriaService
    {
        private readonly ICriteriaRepository criteriaRepository;
        private readonly ICriterionService criterionService;

        public CriteriaService(ICriteriaRepository criteriaRepository, ICriterionService criterionService)
        {
            this.criteriaRepository = criteriaRepository;
            this.criterionService = criterionService;
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

        public async Task<IEnumerable<Criteria?>> GetByContest(Guid? contestId)
        {
            return await criteriaRepository.GetAllAsync(@"WHERE res.""MainObject""->'Contest'->>'Id' = '" + contestId + "'") ?? new List<Criteria>();
        }

        public async Task<IEnumerable<CustomCriteria>?> GetFullCriteriaByContest(Guid? contestId)
        {
            try
            {
                List<CustomCriteria> output = new List<CustomCriteria>();

                var result = await GetByContest(contestId);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var criterions = await criterionService.GetByCriteria(item?.Id) ?? new List<Criterion>();
                        output?.Add(new CustomCriteria
                        {
                            CriteriaInfo = item,
                            CriterionList = new System.Collections.ObjectModel.ObservableCollection<Criterion>(criterions)
                        });
                    }
                }

                return output;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Criteria?> Update(Criteria? criteria, IDbTransaction? transaction = null)
        {
            var obj = Helpers.ObjectHelper<Criteria>.CloneObject(criteria);
            obj.ModifiedOn = DateTime.UtcNow;
            return await criteriaRepository.ExecuteTransactionalAsync(obj, transaction);
        }
    }
}
