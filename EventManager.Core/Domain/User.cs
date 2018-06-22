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
		//public Signature Owner { get; protected set; }
		public Signature Modifier { get; protected set; }

		protected User() { }


		public User(Guid id, string login, string password, string firstName, string lastName, UserRole role, UserStatus status, Signature creator)
		{
			AddUser(id, login, password, firstName, lastName, role, status, creator);
		}
		private void AddUser(Guid id, string login, string password, string firstName, string lastName, UserRole role, UserStatus status, Signature creator)
		{
			Id = id;
			Login = login;
			Creator = creator;
			UpdateUser( firstName, lastName, role, status, creator);
			SetPassword(password);
		}
		public void UpdateUser(string firstName, string lastName, UserRole role, UserStatus status, Signature modifier)
		{
			FirstName = firstName;
			LastName = lastName;
			Role = role;
			Status = status;
			Modifier = modifier;
		}
		public void SetPassword(string password)
		{
			Password = password;
		}
	}
}
