using ObjectLoader.Event;
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
    }
}
