using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices.Event
{
    public interface ICriterionService
    {
        Task<Criterion?> Create(Criterion? criterion, IDbTransaction? transaction = null);
        Task<Criterion?> Update(Criterion? criterion, IDbTransaction? transaction = null);
        Task Delete(Criterion? criterion, IDbTransaction? transaction = null);
        Task<IEnumerable<Criterion?>> GetAll(string? condition = null);
        Task<IEnumerable<Criterion>?> GetByCriteria(Guid? criteriaId);
    }
}
