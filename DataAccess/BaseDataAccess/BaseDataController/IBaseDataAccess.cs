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
        Task<T?> ExecuteAsyncTran(T? entity, IDbConnection connection, IDbTransaction transaction);
        Task<T?> ExecuteAsync(T? entity);
    }
}
