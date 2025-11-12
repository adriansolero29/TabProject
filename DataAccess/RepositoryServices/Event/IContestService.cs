using ObjectLoader.Event;
using RepositoryServices.CustomModel;
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
        Task<Contest?> Create(Contest? contest);
        Task<Contest?> Update(Contest? contest);
        Task Delete(Contest? contest);
        Task<IEnumerable<Contest>?> GetAll(string? condition = null);
    }
}
