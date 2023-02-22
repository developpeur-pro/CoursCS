using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatesDurées
{
	internal class DemoFuseauHoraire
	{
		/// <summary>
		/// Affiche les fuseaux horaires présents sur le poste sous forme de tableau
		/// La colonne E indique si le fuseau a une heure d'été
		/// </summary>
		public static void AfficherFuseaux()
		{
			Console.WriteLine("Id" + new string(' ', 29) + " | E | Nom standard" + new string(' ', 19) + " | Décalage");
			Console.WriteLine(new string('-', 85));

			List<TimeZoneInfo> tzis = TimeZoneInfo.GetSystemTimeZones().ToList();
			foreach (TimeZoneInfo tzi in tzis)
			{
				string desc = $"{tzi.Id,-31} | {(tzi.SupportsDaylightSavingTime ? 'O' : 'N')} | {tzi.StandardName,-31} | {tzi.BaseUtcOffset}";
				Console.WriteLine(desc);
			}

			Console.WriteLine();
			int res = tzis.Where(t => t.SupportsDaylightSavingTime).Count();
			Console.WriteLine("{0} fuseaux avec heure d'été et {1} sans", res, tzis.Count - res);
		}

		/// <summary>
		/// Convertit des dates/heures vers un autre fuseau horaire
		/// </summary>
		public static void TesterConversionVersFuseau()
		{
			DateTimeOffset dto1 = new DateTimeOffset(2030, 1, 31, 14, 0, 0, new TimeSpan(1, 0, 0));
			string idFuseau = "Canada Central Standard Time"; // le code CST fonctionne aussi

			DateTimeOffset dto2 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dto1, idFuseau);
			Console.WriteLine($"Conversion de {dto1} \nvers le fuseau {idFuseau}\n = {dto2}");

			/* Conversion de 31/01/30 14:00:00 +01:00
				vers le fuseau Canada Central Standard Time
				 = 31/01/30 07:00:00 -06:00 */

			DateTime dt1 = new DateTime(2030, 1, 31, 14, 0, 0, DateTimeKind.Utc);
			DateTime dt2 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt1, idFuseau);
			Console.WriteLine($"Conversion de {dt1} \nvers le fuseau {idFuseau}\n = {dt2}");

			/* Conversion de 31/01/30 14:00:00
				vers le fuseau Canada Central Standard Time
				 = 31/01/30 08:00:00 */
		}

		/// <summary>
		/// Ajoute du temps à une date locale en tenant compte du passage à l'heure d'été 
		/// </summary>
		public static void TesterAjoutTemps()
		{
			DateTimeOffset dto = new DateTimeOffset(2030, 3, 30, 23, 0, 0, new TimeSpan(1,0,0));

			// Convertit cette date/heure en temps UTC et lui ajoute le temps souhaité
			TimeSpan ajout = new TimeSpan(7, 30, 0);
			DateTimeOffset dto2 = dto.ToUniversalTime().Add(ajout);

			// Reconvertit le résultat en temps local
			dto2 = dto2.ToLocalTime();

			// Affiche le résultat
			Console.WriteLine($"{dto} + {ajout} = {dto2}");
			// 30/03/30 23:00:00 +01:00 + 7:30 = 31/03/30 07:30:00 +02:00
			// Le passage à l'heure d'été a bien été intégré
		}
	}
}
