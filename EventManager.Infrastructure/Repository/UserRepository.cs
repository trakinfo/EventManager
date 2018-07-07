using EventManager.Core.Domain;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class UserRepository : IUserRepository
	{
		public Task AddAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetAsync(string login)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(User user)
		{
			throw new NotImplementedException();
		}
	}
}
