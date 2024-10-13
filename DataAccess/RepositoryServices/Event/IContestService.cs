using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices.Event
{
    public interface IContestService
    {
        Task<Contest?> Create(Contest? contest, IDbTransaction? transaction = null);
        Task<Contest?> Update(Contest? contest, IDbTransaction? transaction = null);
        Task Delete(Contest? contest, IDbTransaction? transaction = null);
        Task<IEnumerable<Contest>?> GetAll(string? condition = null);
    }
}
