using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class GenericRepository : IRepository
	{
		protected IDataBaseContext dbContext;
		protected ISql sql;

		public GenericRepository(IDataBaseContext context, ISql _sql)
		{
			dbContext = context;
			sql = _sql;
		}

		public async Task AddAsync<T>(object[] sqlParamValue, AddDataParameters createParams)
		{
			await dbContext.AddRecordAsync(sql.Insert(), sqlParamValue, createParams);
		}

		public async Task DeleteAsync<T>(object[] sqlParamValue, AddDataParameters createParams)
		{
			await dbContext.RemoveRecordAsync(sql.Update(), sqlParamValue, createParams);
		}

		public async Task<T> GetAsync<T>(ulong id, GetData<T> Get)
		{
			return await dbContext.FetchRecordAsync(sql.Select(id), Get);
		}

		public async Task<IEnumerable<T>> GetListAsync<T>(string name, GetData<T> Get)
		{
			return await dbContext.FetchRecordSetAsync(sql.SelectMany(name), Get);
		}

		public async Task UpdateAsync<T>(object[] sqlParamValue, AddDataParameters createParams)
		{
			await dbContext.UpdateRecordAsync(sql.Update(), sqlParamValue, createParams);
		}
	}
}
