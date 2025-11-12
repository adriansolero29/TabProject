using BaseDataAccess.BaseDataController;
using BaseDataAccess.EventRepository.Interface;
using DTOs.Event;
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

        public Task<Criteria?> Create(Criteria? criteria, IDbTransaction? transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task Create(CustomCriteria? criteria)
        {
            throw new NotImplementedException();
        }

        public async Task CreateFullCriteria(CustomCriteria? obj)
        {
            try
            {
                var connection = DataManipulator<Criteria, CriteriaDTO>.OpenConnection();
                var transaction = DataManipulator<Criteria, CriteriaDTO>.BeginTransaction();
                var criteria = await criteriaRepository.ExecuteAsyncTran(obj?.CriteriaInfo, connection, transaction);

                var criterions = obj?.CriterionList;
                if (criterions != null)
                {
                    foreach (var item in criterions)
                    {
                        item.Criteria = criteria;
                        await criterionService.CreateTran(item, connection, transaction);
                    }
                }

                DataManipulator<Criteria, CriteriaDTO>.CommitTransaction();
                DataManipulator<Criteria, CriteriaDTO>.CloseConnections();
            }
            catch (Exception ex)
            {
                DataManipulator<Criteria, CriteriaDTO>.CloseConnections();
                throw;
            }
        }

        public Task Delete(Criteria? criteria, IDbTransaction? transaction = null)
        {
            throw new NotImplementedException();
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

        public Task<Criteria?> Update(Criteria? criteria, IDbTransaction? transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
