using BaseDataAccess.BaseDataController.Connection;
using BaseDataAccess.EventRepository.Interface;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryServices.Event
{
    public class ContestService : IContestService
    {
        private readonly IContestRepository contestRepository;

        public ContestService(IContestRepository contestRepository)
        {
            this.contestRepository = contestRepository;
        }

        public async Task<Contest?> Create(Contest? contest, IDbTransaction? transaction = null)
        {
            var result = await contestRepository.CustomExecuteTransactionalAsync(contest, transaction);
            return result;
        }

        public async Task Delete(Contest? contest, IDbTransaction? transaction = null)
        {
            var obj = Helpers.ObjectHelper<Contest>.CloneObjectJson(contest);
            obj.IsDeleted = true;
            await contestRepository.CustomExecuteTransactionalAsync(obj);
        }

        public async Task<IEnumerable<Contest>?> GetAll(string? condition = null)
        {
            try
            {
                var result = await contestRepository.GetAllAsync();
                return result ?? new List<Contest>();
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<Contest?> Update(Contest? contest, IDbTransaction? transaction = null)
        {
            var obj = Helpers.ObjectHelper<Contest>.CloneObject(contest);
            obj.ModifiedOn = DateTime.UtcNow;
            return await contestRepository.CustomExecuteTransactionalAsync(obj, transaction);
        }
    }
}
