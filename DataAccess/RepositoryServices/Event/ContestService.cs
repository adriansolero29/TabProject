using BaseDataAccess.BaseDataController;
using BaseDataAccess.BaseDataController.Connection;
using BaseDataAccess.EventRepository.Interface;
using DTOs.Event;
using ObjectLoader.Event;
using RepositoryServices.CustomModel;
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

        public async Task<Contest?> Create(Contest? contest)
        {
            if (contest?.Id != null)
            {
                contest.Version = contest.Version + 1;
                contest.ModifiedOn = DateTime.UtcNow;
            }

            return await contestRepository.ExecuteAsync(contest);
        }
        public async Task Delete(Contest? contest)
        {
            if (contest != null)
                contest.IsDeleted = true;

            await contestRepository.ExecuteAsync(contest);
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

        public async Task<Contest?> Update(Contest? contest)
        {
            return await contestRepository.ExecuteAsync(contest);
        }
    }
}
