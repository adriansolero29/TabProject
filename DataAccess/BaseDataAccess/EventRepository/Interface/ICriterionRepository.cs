using BaseDataAccess.BaseDataController;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseDataAccess.EventRepository.Interface
{
    public interface ICriterionRepository : IBaseDataAccess<Criterion>
    {
        Task<IEnumerable<Criterion>?> GetByCriteria(Guid? criteriaId);
    }
}
