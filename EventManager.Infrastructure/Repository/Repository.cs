using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Repository;
using System.Collections.Generic;
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

		public async Task<IEnumerable<T1>> GetListAsync<T1>(string name, object[] sqlParamValue, DataParameters createSelectParams, GetData<T1> Get)
		{
			return await dbContext.FetchRecordSetAsync(sql.SelectMany(name), sqlParamValue, createSelectParams, Get);
		}

		public async Task AddAsync(object[] sqlParamValue, DataParameters createInsertParams)
		{
			await dbContext.AddRecordAsync(sql.Insert(), sqlParamValue, createInsertParams);
		}

		public async Task<int> AddManyAsync(ISet<object[]> sqlParamValue, DataParameters createInsertParams)
		{
			return await dbContext.AddManyRecordsAsync(sql.Insert(), sqlParamValue, createInsertParams);
		}

		public async Task<int> UpdateAsync(object[] sqlParamValue, DataParameters createUpdateParams)
		{
			return await dbContext.UpdateRecordAsync(sql.Update(), sqlParamValue, createUpdateParams);
		}

		public async Task<int> DeleteAsync(object[] sqlParamValue, DataParameters createDeleteParams)
		{
			return await dbContext.RemoveRecordAsync(sql.Delete(), sqlParamValue, createDeleteParams);
		}

	}
}
