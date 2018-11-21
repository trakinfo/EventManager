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
		public UserRepository(IDataBaseContext context, IUserSql userSql) : base(context, userSql)
		{
			//RefreshRepo();
		}

		//private void RefreshRepo()
		//{
		//	contentList = UserList;
		//}

		private User CreateUser(IDataReader R)
		{
			throw new NotImplementedException();
		}
	}
}
