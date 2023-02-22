using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatesDurées
{
	internal class DemoDateTimeOffset
	{
		public static void TesterPropriétés()
		{
			DateTimeOffset dto1 = new DateTimeOffset(2030, 1, 31, 22, 45, 58, new TimeSpan());
			Console.WriteLine($"DateTimeOffset: {dto1}");               // 31/01/30 22:45:58 +00:00

			//------------------------------------------------------------
			Console.WriteLine("\nRécupération des différentes parties :");

			Console.WriteLine($"Date : {dto1.Date}");					// 31/01/30 00:00:00
			Console.WriteLine($"TimeOfDay : {dto1.TimeOfDay}");   // 22:45:58
			Console.WriteLine($"Offset : {dto1.Offset}");         // 00:00:00
			Console.WriteLine("{0}/{1}/{2} {3}:{4}:{5}.{6}",
				dto1.Year, dto1.Month, dto1.Day, dto1.Hour, dto1.Minute, dto1.Second, dto1.Millisecond);
			
			//------------------------------------------------------------
			Console.WriteLine("\nRécupération de la partie date/heure dans différents systèmes de temps");

			Console.WriteLine($"DateTime      : {dto1.DateTime}");      // 31/01/30 22:45:58
			Console.WriteLine($"LocalDateTime : {dto1.LocalDateTime}"); // 31/01/30 23:45:58
			Console.WriteLine($"UtcDatetime   : {dto1.UtcDateTime}");   // 31/01/30 22:45:58

			//------------------------------------------------------------
			Console.WriteLine("\nRécupération du jour dans la semaine ou l'année");

			Console.WriteLine(dto1.DayOfWeek);  // Thursday
			Console.WriteLine(dto1.DayOfYear);  // 31
		}

		public static void TesterMéthodes()
		{
			//------------------------------------------------------------
			Console.WriteLine("\nMéthodes Add...");
			DateTimeOffset dto1 = new DateTimeOffset(2030, 1, 31, 22, 45, 58, new TimeSpan());
			DateTimeOffset dto2 = dto1.AddYears(-1).AddMonths(-1).AddHours(-1);
			Console.WriteLine(dto2);   // 31/12/28 21:45:58 +00:00

			//------------------------------------------------------------
			Console.WriteLine("\nOpérateurs +  et -");

			TimeSpan ts1 = new TimeSpan(1, 1, 1, 1); // 1j, 1h, 1min, 1s
			Console.WriteLine(dto1 + ts1);   // 01/02/30 23:46:59 +00:00
			Console.WriteLine(dto1 - ts1);   // 30/01/30 21:44:57 +00:00

			//------------------------------------------------------------
			Console.WriteLine("\nComparaison");

			TimeSpan tsDecalage = new TimeSpan(3, 0, 0);
			DateTimeOffset dto3 = new DateTimeOffset(dto1.DateTime, tsDecalage);
			Console.WriteLine(dto3 < dto1);  // True

			//------------------------------------------------------------
			Console.WriteLine("\nConversion dans un système de temps différent");

			Console.WriteLine($"Aucun : {dto1}");                     // 31/01/30 22:45:58 +00:00
			Console.WriteLine($"Local : {dto1.ToLocalTime()}");       // 31/01/30 23:45:58 +01:00
			Console.WriteLine($"UTC   : {dto1.ToUniversalTime()}");   // 31/01/30 22:45:58 +00:00

			Console.WriteLine("\nCas particulier du passage à l'heure d'été");
			DateTime dtHeureEté = new DateTime(2030, 3, 31, 2, 0, 0); // 31/03/2030 à 2:00
			TimeSpan tsHeureEté = new TimeSpan(1, 0, 0); // décalage de 1h par rapport au temps UTC
			DateTimeOffset dtoHeureEté = new DateTimeOffset(dtHeureEté, tsHeureEté);

			Console.WriteLine(dtoHeureEté);                    // 31/03/30 02:00:00 +01:00
			Console.WriteLine(dtoHeureEté.ToLocalTime());      // 31/03/30 03:00:00 +02:00
			Console.WriteLine(dtoHeureEté.ToUniversalTime());  // 31/03/30 01:00:00 +00:00
		}
	}
}
