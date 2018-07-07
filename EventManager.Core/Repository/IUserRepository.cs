﻿using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface IUserRepository
    {
		Task<User> GetAsync(Guid userId);
		Task<User> GetAsync(string login);
	
		Task AddAsync(User user);
		Task UpdateAsync(User user);
		Task DeleteAsync(Guid userId);
	}
}