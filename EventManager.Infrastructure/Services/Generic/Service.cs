using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;

namespace EventManager.Infrastructure.Services.Generic
{
	public class Service : IService
	{
		protected IDataBaseContext dbContext;
		protected ISql sql;

		public Service(IDataBaseContext _context, ISql _sql)
		{
			dbContext = _context;
			sql = _sql;
		}

		public async Task<T> GetAsync<T>(ulong id, GetData<T> GetRow)
		{
			return await dbContext.FetchRecordAsync(sql.Select(id), GetRow);
		}

		public async Task<IEnumerable<T>> GetListAsync<T>(string name, GetData<T> GetRow)
		{
			return await dbContext.FetchRecordSetAsync(sql.SelectMany(name), GetRow);
		}

		public async Task AddAsync(object[] sqlParamValue, DataParameters createParams)
		{
			await dbContext.AddRecordAsync(sql.Insert(), sqlParamValue, createParams);
		}

		public async Task UpdateAsync(object[] sqlParamValue, DataParameters updateParams)
		{
			await dbContext.UpdateRecordAsync(sql.Update(), sqlParamValue, updateParams);
		}

		public async Task DeleteAsync(object[] sqlParamValue, DataParameters deleteParams)
		{
			await dbContext.RemoveRecordAsync(sql.Update(), sqlParamValue, deleteParams);
		}
	}
}
