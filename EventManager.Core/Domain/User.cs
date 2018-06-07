using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{

	class User : Entity
	{
		public string Login { get; protected set; }
		public string Password { get; protected set; }
		public string FirstName { get; protected set; }
		public string LastName { get; protected set; }
		public UserRole Role { get; protected set; }
		public UserStatus Status { get; protected set; }
		public Signature Owner { get; protected set; }
		public Signature Modifier { get; protected set; }

		protected User() { }


		public User(Guid id, string login, string password, string firstName, string lastName, UserRole role, UserStatus status, Signature owner)
		{
			AddUser(id, login, password, firstName, lastName, role, status, owner);
		}
		private void AddUser(Guid id, string login, string password, string firstName, string lastName, UserRole role, UserStatus status, Signature owner)
		{
			Id = id;
			Login = login;
			FirstName = firstName;
			LastName = lastName;
			Role = role;
			Status = status;
			Owner = owner;
			Modifier = Owner;
		}
		public void UpdateUser()
		{

		}
	}
}
