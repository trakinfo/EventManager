using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class Repository<T> : IRepository
	{
		protected IDataBaseContext dbContext;
		protected ISql sql;
		//protected IEnumerable<T> contentList;


		public Repository(IDataBaseContext context, ISql _sql)
		{
			dbContext = context;
			sql = _sql;
		}

		public async Task<T1> GetAsync<T1>(long id, GetData<T1> Get)
		{
			return await dbContext.FetchRecordAsync(sql.Select(id), Get);
		}

		public async Task<IEnumerable<T1>> GetListAsync<T1>(string name, GetData<T1> Get)
		{
			return await dbContext.FetchRecordSetAsync(sql.SelectMany(name), Get);
		}

		public async Task AddAsync(object[] sqlParamValue, DataParameters createParams)
		{
			await dbContext.AddRecordAsync(sql.Insert(), sqlParamValue, createParams);
		}

		public async Task AddManyAsync(ISet<object[]> sqlParamValue, DataParameters createParams)
		{
			await dbContext.AddManyRecordsAsync(sql.Insert(), sqlParamValue, createParams);
		}

		public async Task UpdateAsync(object[] sqlParamValue, DataParameters createParams)
		{
			await dbContext.UpdateRecordAsync(sql.Update(), sqlParamValue, createParams);
		}

		public async Task DeleteAsync(object[] sqlParamValue, DataParameters createParams)
		{
			await dbContext.RemoveRecordAsync(sql.Update(), sqlParamValue, createParams);
		}

	}
}
