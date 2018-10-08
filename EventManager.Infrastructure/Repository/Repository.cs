using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class Repository : IRepository
	{
		protected IDataBaseContext dbContext;
		protected ISql sql;

		public event EventHandler RecordAffected;

		public Repository(IDataBaseContext context, ISql _sql)
		{
			dbContext = context;
			sql = _sql;
		}

		public async Task<T> GetAsync<T>(long id, GetData<T> Get)
		{
			return await dbContext.FetchRecordAsync(sql.Select(id), Get);
		}

		public async Task<IEnumerable<T>> GetListAsync<T>(string name, GetData<T> Get)
		{
			return await dbContext.FetchRecordSetAsync(sql.SelectMany(name), Get);
		}

		public async Task AddAsync(object[] sqlParamValue, DataParameters createParams)
		{
			await dbContext.AddRecordAsync(sql.Insert(), sqlParamValue, createParams);
			RecordAffected?.Invoke(this, new EventArgs());
		}

		public async Task UpdateAsync(object[] sqlParamValue, DataParameters createParams)
		{
			await dbContext.UpdateRecordAsync(sql.Update(), sqlParamValue, createParams);
			RecordAffected?.Invoke(this, new EventArgs());
		}

		public async Task DeleteAsync(object[] sqlParamValue, DataParameters createParams)
		{
			await dbContext.RemoveRecordAsync(sql.Update(), sqlParamValue, createParams);
			RecordAffected?.Invoke(this, new EventArgs());
		}

	}
}
