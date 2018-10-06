using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.SQL
{
	public static class UserSql
	{
		public static string SelectSysUser()
		{
			return "SELECT USER();";
		}
		public static string SelectUsers()
		{
			return "SELECT u.Login, u.Nazwisko, u.Imie, u.Sex, u.Faximile, u.email, u.Password, u.Role, u.Status FROM `user` u WHERE u.Role>0;";
		}
		public static string SelectAllUsers()
		{
			return "SELECT u.Login, u.Nazwisko, u.Imie, u.Sex, u.Faximile, u.email, u.Password, u.Role, u.Status, u.Owner, u.User, u.ComputerIP, u.Version FROM `user` u;";
		}
		public static string SelectSomeUsers(string SchoolID)
		{
			return "SELECT u.Login, u.Nazwisko, u.Imie, u.Sex, u.Faximile, u.email, u.Password, u.Role, u.Status, u.Owner, u.User, u.ComputerIP, u.Version FROM `user` u WHERE u.Role>0 AND u.Login NOT IN (SELECT sn.Nauczyciel FROM szkola_nauczyciel sn WHERE sn.IdSzkola ='" + SchoolID + "');";
		}
		public static string InsertUser()
		{
			return "INSERT INTO user (Login,Nazwisko,Imie,Email,Password,Role,Status,Sex,Owner,User,ComputerIP) VALUES (@Login,@LastName,@FirstName,@Email,@Password,@Role,@Status,@Sex,@Owner,@User,@IP);";
		}

		public static string UpdateUser()
		{
			return "UPDATE user SET Nazwisko=@Nazwisko,Imie=@Imie,Email=@Email,Role=@Rola,Status=@Status,Sex=@Sex,User=@User,ComputerIP=@IP WHERE Login=@Login;";
		}
		public static string DeleteUser()
		{
			return "DELETE FROM user WHERE Login=@Login;";
		}
		public static string CountUser(string Login)
		{
			return "SELECT COUNT(Login) FROM user WHERE Login='" + Login + "';";
		}
	}
}
