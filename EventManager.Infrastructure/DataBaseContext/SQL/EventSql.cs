using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.SQL
{
    public static class EventSql
    {
		internal static string SelectEvent(string name)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version,l.ID As IdLocation,l.Name As LocationName, l.PhoneNumber, l.Email, l.www, a.PlaceName, a.StreetName, a.PropertyNumber, a.ApartmentNumber, a.PostalCode, a.PostOffice FROM `event` e LEFT JOIN location l ON l.ID = e.IdLocation LEFT JOIN address a ON a.ID=l.IdAddress WHERE e.Name LIKE '{name}%' ORDER BY e.Name;";
		}

		internal static string SelectLocation(ulong ID)
		{
			return $"SELECT l.Name, l.PhoneNumber, l.Email, l.www FROM location l WHERE l.ID={ID}";
		}

		internal static string SelectAddress(ulong ID)
		{
			return $"SELECT a.PlaceName, a.StreetName, a.PropertyNumber, a.ApartmentNumber, a.PostalCode, a.PostOffice FROM address a WHERE a.ID={ID}";
		}

		internal static string SelectSector(ulong idLocation)
		{
			return $"SELECT s.ID,s.Name,s.Description,s.SeatingCount FROM sector s WHERE s.IdLocation={idLocation};";
		}

		internal static string SelectTicket(ulong idEvent)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,IdSector FROM ticket t WHERE IdEvent={idEvent};";
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
