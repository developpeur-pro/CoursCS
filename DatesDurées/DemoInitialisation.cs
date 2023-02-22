using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatesDurées
{
	internal class DemoInitialisation
	{
		public static void TesterInitialisation()
		{
			// Dates/heures
			DateTime dt0 = new DateTime(); // 01/01/0001 00:00:00 (= DateTime.MinValue)
			DateTime dt1 = new DateTime(2030, 6, 21); // 21/06/2030 00:00:00
			DateTime dt2 = new DateTime(2030, 12, 25, 22, 30, 0); // 25/12/2030 22:30:00
			DateTime dt3 = new DateTime(2030, 12, 25, 22, 30, 0, DateTimeKind.Utc); // temps UTC
			DateTime dt4 = DateTime.Today; // Date du jour et heure à 0
			DateTime dt5 = DateTime.UtcNow; // Date et heure du jour au temps UTC

			Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n", dt0, dt1, dt2, dt3, dt4, dt5);

			// Durées
			TimeSpan ts0 = new TimeSpan();   // 00:00:00
			TimeSpan ts1 = new TimeSpan(3, 0, 0); // 03:00:00
			TimeSpan ts2 = new TimeSpan(4, 23, 41, 58); // 4.23:41:58 (jour.hh:mm:ss)
			TimeSpan ts3 = new TimeSpan(4, 23, 41, 62); // 4.23:42:02
			Console.WriteLine("{0}\n{1}\n{2}\n{3}\n", ts0, ts1, ts2, ts3);

			// Date/heures avec décalage par rapport au temps UTC
			DateTimeOffset dto0 = new DateTimeOffset(dt0, ts0); // 01/01/01 00:00:00 +00:00
			DateTimeOffset dto1 = new DateTimeOffset(dt1, ts1); // 21/06/30 00:00:00 +03:00
			DateTimeOffset dto2 = new DateTimeOffset(dt2, ts1); // 25/12/30 22:30:00 +03:00

			//DateTimeOffset dto3 = new DateTimeOffset(dt2, ts2); /* erreur, car le décalage doit être en minutes entières */
			//DateTimeOffset dto4 = new DateTimeOffset(dt3, ts1); /* erreur, car pour une date UTC, le décalage doit être de 0 */			
			Console.WriteLine("{0}\n{1}\n{2}\n", dto0, dto1, dto2);

			// Dates seules
			DateOnly do0 = new DateOnly();   // 01/01/0001
			DateOnly do1 = new DateOnly(2030, 09, 22); // 22/09/2030
			DateOnly do2 = DateOnly.FromDateTime(DateTime.Today); // Date du jour
			Console.WriteLine("{0}\n{1}\n{2}\n", do0, do1, do2);

			// Heures seules
			TimeOnly to0 = new TimeOnly();   //	00:00:00
			TimeOnly to1 = new TimeOnly(14, 55, 28);  // 14:55:28
			TimeOnly to2 = TimeOnly.FromDateTime(DateTime.Now);   // Heure courante
			Console.WriteLine("{0:T}\n{1:T}\n{2:T}\n", to0, to1, to2);
		}

		public static void TesterAnalyseChaine()
		{
			const string erreur = "Impossible d'interpréter la chaîne en date";
			bool res;

			// Dates/heures
			string sdt1 = "25/12/2030 22:30:45"; // Format par défaut de la culture courante (fr-FR)
			res = DateTime.TryParse(sdt1, out DateTime dt1);
			Console.WriteLine(res ? dt1 : erreur); // 25/12/2030 22:30:45

			string sdt11 = "25 12 2030 22:30:45"; // Autre séparateur
			res = DateTime.TryParse(sdt11, out DateTime dt11);
			Console.WriteLine(res ? dt11 : erreur); // 25/12/2030 22:30:45

			string sdt2 = "12/25/2030 10:30:45 PM"; // Format de la culture en-US
			res = DateTime.TryParse(sdt2, CultureInfo.GetCultureInfo("en-US"), out DateTime dt2);
			Console.WriteLine(res ? dt2 : erreur); // 25/12/2030 22:30:45

			string sdt21 = "12-25-2030 22:30:45";	// Autre format accepté
			res = DateTime.TryParse(sdt21, CultureInfo.GetCultureInfo("en-US"), out DateTime dt21);
			Console.WriteLine(res ? dt21 : erreur); // 25/12/2030 22:30:45

			string sdt3 = "12/25/2030 22:30:45"; // Format par défaut de la culture invariante
			res = DateTime.TryParse(sdt3, CultureInfo.InvariantCulture, out DateTime dt3);
			Console.WriteLine(res ? dt3 : erreur); // 25/12/2030 22:30:45

			string sdt31 = "2030-12-25 22:30:45"; // Autre format reconnu et moins ambigü
			res = DateTime.TryParse(sdt31, CultureInfo.InvariantCulture, out DateTime dt31);
			Console.WriteLine(res ? dt31 : erreur); // 25/12/2030 22:30:45

			// On peut interpréter une date/heure dans un format personnalisé avec TryParseExact en fournissant :
			// - la chaîne de format
			// - une éventuelle culture
			// - des spécifications de styles du format
			string sdtp = "Mercredi 25 déc. 2030 22:30"; // Format personalisé
			string format = "dddd dd MMM yyyy HH:mm";
			res = DateTime.TryParseExact(sdtp, format, CultureInfo.GetCultureInfo("fr-FR"),
												DateTimeStyles.AllowInnerWhite, out DateTime dtp);
			Console.WriteLine(res ? dtp : erreur); // 25/12/30 22:30:00


			// Dates/heures avec décalage horaire
			// Même principe que DateTime avec décalage horaire en +
			string sdto = "2030-12-25 22:30:45 +04:30"; // Format reconnu dans la culture invariante
			res = DateTimeOffset.TryParse(sdto, CultureInfo.InvariantCulture, out DateTimeOffset dto);
			Console.WriteLine(res ? dto : erreur);

			// Dates seules
			// Même principe que DateTime sans la partie heure
			string sdo = "2030-12-25";
			res = DateOnly.TryParse(sdo, CultureInfo.InvariantCulture, out DateOnly don);
			Console.WriteLine(res ? don : erreur);

			// Heures seules
			string sto = "22:30:45";
			res = TimeOnly.TryParse(sto, CultureInfo.InvariantCulture, out TimeOnly ton);
			Console.WriteLine(res ? ton : erreur);

			// Durées (format moins dépendant de la culture)
			string sts = "1.23:42:50";
			res = TimeSpan.TryParse(sts, CultureInfo.InvariantCulture, out TimeSpan ts);
			Console.WriteLine(res ? ts : erreur);

			string sts2 = "1j 23h 42min 50s";	// Format personnalisé
			string format2 = "d'j 'hh'h 'mm'min 'ss's'";
			res = TimeSpan.TryParseExact(sts2, format2, CultureInfo.GetCultureInfo("fr-FR"), out TimeSpan ts2);
			Console.WriteLine(res ? ts2 : erreur);
		}
	}
}
