using ObjectLoader.Event;
using RepositoryServices.CustomModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices.Event
{
    public interface ICriteriaService
    {
        Task<Criteria?> Create(Criteria? criteria, IDbTransaction? transaction = null);
        Task<Criteria?> Update(Criteria? criteria, IDbTransaction? transaction = null);
        Task Delete(Criteria? criteria, IDbTransaction? transaction = null);
        Task<IEnumerable<Criteria?>> GetAll(string? condition = null);
        Task<IEnumerable<Criteria?>> GetByContest(Guid? contestId);
        Task<IEnumerable<CustomCriteria>?> GetFullCriteriaByContest(Guid? contestId);
    }
}
