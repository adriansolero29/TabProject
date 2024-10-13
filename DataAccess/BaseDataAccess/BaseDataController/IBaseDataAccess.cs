using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BaseDataAccess.BaseDataController
{
    public interface IBaseDataAccess<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid? id, string? condition = null);
        Task<IEnumerable<T>?> GetAllAsync(string? condition = null);
        Task<T?> ExecuteTransactionalAsync(T? entity, IDbTransaction? transaction);
        Task<T?> CustomExecuteAsync(T? entity = null);
        Task<T?> CustomExecuteTransactionalAsync(T? entity = null, IDbTransaction? transaction = null);
    }
}
