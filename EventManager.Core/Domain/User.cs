using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
	public enum UserRole { User, Operator, Admin}
	public enum UserStatus { Nieaktywny, Aktywny}
    class User : Entity
    {
		public string Login { get; protected set; }
		public string Password { get; protected set; }
		public string FirstName { get; protected set; }
		public string LastName { get; protected set; }
		public UserRole Role { get; protected set; }
		public UserStatus Status { get; protected set; }
		public Signature Modifier { get; protected set; }

		protected User() { }

		 
		public User(Guid id, string login, string password, string firstName, string lastName, UserRole role, UserStatus status, Signature modifier)
		{
			Id = id;
			Login = login;
			FirstName = firstName;
			LastName = lastName;
			Role = role;
			Status = status;
			Modifier = modifier;
		}
    }
}
