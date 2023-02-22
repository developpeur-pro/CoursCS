using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatesDurées
{
	internal class DemoTimeSpan
	{
		public static void TesterPropriétés()
		{
			//------------------------------------------------------------
			Console.WriteLine("Propriétés Days, Hours, Minutes...");

			TimeSpan ts1 = new TimeSpan(2, 23, 45, 58);
			Console.WriteLine($"{ts1.Days}.{ts1.Hours}:{ts1.Minutes}:{ts1.Seconds}");

			//------------------------------------------------------------
			Console.WriteLine("\nPropriétés TotalDays, TotalHours");

			Console.WriteLine($"TotalDays : {ts1.TotalDays}");    // 2,99025...
			Console.WriteLine($"TotalHours : {ts1.TotalHours}");   // 71,76611..
			Console.WriteLine($"TotalMinutes : {ts1.TotalMinutes}");   // 4305,966..

			//------------------------------------------------------------
			Console.WriteLine("\nConstantes");

			Console.WriteLine(TimeSpan.Zero);      // 00:00:00
			Console.WriteLine(TimeSpan.MinValue);  // -10675199.02:48:05.4775808
			Console.WriteLine(TimeSpan.MaxValue);  // 10675199.02:48:05.4775807
			Console.WriteLine(TimeSpan.TicksPerDay);  // 864000000000
		}

		public static void TesterMéthodes()
		{
			//------------------------------------------------------------
			Console.WriteLine("Méthodes FromDays, FromHours, FromMinutes");

			Console.WriteLine(TimeSpan.FromDays(2.990255));    // 2.23:45:58.032
			Console.WriteLine(TimeSpan.FromHours(71.7661111)); // 2.23:45:57.999
			Console.WriteLine(TimeSpan.FromMinutes(4305.9667)); // 2.23:45:58.002

			//------------------------------------------------------------
			Console.WriteLine("\nAjouter ou retrancher un autre intervalle de temps");

			TimeSpan ts1 = new TimeSpan(1, 9, 10, 20);
			TimeSpan ts2 = new TimeSpan(1, 1, 1, 1); // 1.01:01:01
			Console.WriteLine(ts1.Add(ts2));       // 2.10:11:21
			Console.WriteLine(ts1.Subtract(ts2));  // 08:09:19
			Console.WriteLine(ts1 + ts2);   // 2.10:11:21
			Console.WriteLine(ts1 - ts2);   // 08:09:19

			//------------------------------------------------------------
			Console.WriteLine("\nMéthodes Negate, Duration, Multiply, Divide");

			//TimeSpan ts1 = new TimeSpan(1, 9, 10, 20);
			Console.WriteLine(ts1.Negate());		// -1.9:10:20
			Console.WriteLine(ts1.Duration());	// 1.9:10:20
			Console.WriteLine(ts1.Multiply(2)); // 2.18:20:40
			Console.WriteLine(ts1.Divide(2));	// 16:35:10
		}
	}
}
