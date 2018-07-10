using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.SQL
{
    public static class EventSql
    {
		internal static string SelectEvent()
		{
			return $"SELECT e.ID,e.Name,e.Description,e.StartDate,e.EndDate,l.Name As LocationName,l.PhoneNumber,l.Email,l.Www,e.User,e.HostIP,e.Version FROM `event` e INNER JOIN location l ON l.ID=e.IdLocation ORDER BY e.Name;";
		}
		internal static string SelectEvent(string Name)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.StartDate,e.EndDate,l.Name,l.PhoneNumber,l.Email,l.Www FROM `event` e INNER JOIN location l ON l.ID=e.IdLocation WHERE e.Name='{Name}' ORDER BY e.Name;";
		}
		internal static string InsertEvent()
		{
			return "INSERT INTO event VALUES(null,?Name,?Description,?IdLocation,?StartDate,?EndDate,?User,?HostIP,NULL);";
		}

		internal static string UpdateEvent()
		{
			return "UPDATE uczen SET Nazwisko=?Nazwisko, Imie=?Imie, Imie2=?Imie2, NrArkusza=?NrArkusza, ImieOjca=?ImieOjca, NazwiskoOjca=?NazwiskoOjca, ImieMatki=?ImieMatki, NazwiskoMatki=?NazwiskoMatki, DataUr=?DataUr, Pesel=?Pesel, IdMiejsceUr=?IdMiejsceUr, IdMiejsceZam=?IdMiejsceZam, Ulica=?Ulica, NrDomu=?NrDomu, NrMieszkania=?NrMieszkania, Tel=?Tel, TelKom1=?TelKom1, TelKom2=?TelKom2, Man=?Man, User=?User, ComputerIP=?IP WHERE ID=?ID;";
		}

		internal static string DeleteEvent()
		{
			return "DELETE FROM uczen WHERE ID=?ID";
		}
	}
}
