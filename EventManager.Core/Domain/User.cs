using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;


namespace EventManager.Core.Domain
{

	public class User : Entity
	{
		private ISet<Signature> _modifier = new HashSet<Signature>();

		public string Login { get; protected set; }
		public byte[] Password { get; protected set; }
		public string FirstName { get; protected set; }
		public string LastName { get; protected set; }
		public UserRole Role { get; protected set; }
		public UserStatus Status { get; protected set; }
		public IEnumerable<Signature> Modifier  => _modifier; 

		protected User() { }

		public User(long id, string login, string password, string firstName, string lastName, UserRole role, UserStatus status, Signature creator)
		{
			AddUser(id, login, password, firstName, lastName, role, status, creator);
		}
		private void AddUser(long id, string login, string password, string firstName, string lastName, UserRole role, UserStatus status, Signature creator)
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
			_modifier.Add(modifier);
		}
		public void SetPassword(string password)
		{
			//todo: zrobić hasło
			Password = Encoding.UTF8.GetBytes(password);
		}
		public void SetRole(UserRole role)
		{
			Role = role;
		}
		public void SetStatus(UserStatus status)
		{
			Status = status;
		}
	}
}
