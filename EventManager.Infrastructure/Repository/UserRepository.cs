using EventManager.Core.DataBaseContext;
using EventManager.Core.Domain;
using EventManager.Core.Repository;
using System.Collections.Generic;

namespace EventManager.Infrastructure.Repository
{
	public class UserRepository : Repository, IUserRepository
	{
		public IEnumerable<User> UserList { get; set; }
		public UserRepository(IDataBaseContext context, IUserSql userSql) : base(context, userSql)
		{
			RefreshRepo();
			RecordAffected -= (s, ex) => RefreshRepo();
			RecordAffected += (s, ex) => RefreshRepo();
		}

		private void RefreshRepo()
		{
			UserList = GetListAsync(null, CreateUser).Result;
		}
	}
}
