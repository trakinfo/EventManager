using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace EventManager.Infrastructure.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		//IEnumerable<User> userList;
		public IEnumerable<User> UserList { get => contentList;}
		public UserRepository(IDataBaseContext context, IUserSql userSql) : base(context, userSql)
		{
			RefreshRepo();
			//RecordAffected -= (s, ex) => RefreshRepo();
			//RecordAffected += (s, ex) => RefreshRepo();
		}

		private void RefreshRepo()
		{
			contentList = UserList;
		}

		private User CreateUser(IDataReader R)
		{
			throw new NotImplementedException();
		}
	}
}
