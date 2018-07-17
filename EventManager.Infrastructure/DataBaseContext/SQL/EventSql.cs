using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.SQL
{
    public static class EventSql
    {
		internal static string SelectEvent(string name)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version,l.ID As IdLocation,l.Name As LocationName, l.PhoneNumber, l.Email, l.Www FROM `event` e INNER JOIN location l ON l.ID = e.IdLocation WHERE e.Name LIKE '{name}%' ORDER BY e.Name;";
		}

		internal static string SelectSector(ulong idLocation)
		{
			return $"SELECT s.ID,s.Name,s.Descrription,s.SeatingCount FROM sector s WHERE s.IdLocation={idLocation};";
		}

		internal static string SelectTicket(ulong idEvent)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,IdSectior FROM ticket t WHERE IdEvent={idEvent};";
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
