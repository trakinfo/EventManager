using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.SQL
{
    public static class EventSql
    {
		internal static string SelectEvent(string Name)
		{
			return $"SELECT p.ID, p.IdUczen, p.IdKlasa, IFNULL(p.NrwDzienniku,0) AS NrwDzienniku, p.Promocja, p.StatusAktywacji, p.DataAktywacji, p.DataDeaktywacji, p.MasterRecord, u.Nazwisko, u.Imie, u.Imie2, u.NrArkusza, u.ImieOjca, u.NazwiskoOjca, u.ImieMatki, u.NazwiskoMatki, u.DataUr, u.Pesel, u.IdMiejsceUr, u.IdMiejsceZam, u.Ulica, u.NrDomu, u.NrMieszkania, u.Tel, u.TelKom1, u.TelKom2, IFNULL(u.Man,0) AS Man, sk.KodKlasy, sk.NazwaKlasy, sk.Virtual,m.Kod AS KodUr,m.Nazwa AS NazwaUr,m.Polska AS PolskaUr,m.Poczta AS PocztaUr,m.KodPocztowy AS KodPocztowyUr,m1.Nazwa AS NazwaZam,m1.Kod AS KodZam,m1.Polska AS PolskaZam,m1.Poczta AS PocztaZam,m1.KodPocztowy AS KodPocztowyZam,u.Owner,u.User,u.ComputerIP,u.Version FROM przydzial p INNER JOIN uczen u ON p.IdUczen = u.ID INNER JOIN szkola_klasa sk ON p.IdKlasa = sk.ID LEFT JOIN miejscowosc m ON u.IdMiejsceUr = m.ID LEFT JOIN miejscowosc m1 ON u.IdMiejsceZam = m1.ID WHERE sk.IdSzkola = '{SchoolId}' AND sk.RokSzkolny = '{SchoolYear}';";
		}

		internal static string InsertEvent()
		{
			return "INSERT INTO uczen VALUES(null,?Nazwisko,?Imie,?Imie2,?NrArkusza,?ImieOjca,?NazwiskoOjca,?ImieMatki,?NazwiskoMatki,?DataUr,?Pesel,?IdMiejsceUr,?IdMiejsceZam,?Ulica,?NrDomu,?NrMieszkania,?Tel,?TelKom1,?TelKom2,?Man,?Owner,?User,?IP,NULL);";
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
