using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumérations
{
	internal class DemoEnums
	{
		/// <summary>
		/// Montre comment utiliser une énumération définie par .Net
		/// </summary>
		public static void TesterEnums()
		{
			string message;

			DateTime dt = DateTime.Now;
			switch (dt.DayOfWeek)
			{
				case DayOfWeek.Friday:
					message = "Bientôt en week-end !";
					break;

				case DayOfWeek.Saturday:
				case DayOfWeek.Sunday:
					message = "Profite bien de ton week-end !";
					break;

				default:
					message = $"Encore {DayOfWeek.Saturday - dt.DayOfWeek} jours avant le week-end...";
					break;
			}

			Console.WriteLine($"Aujourd'hui c'est {dt:dddd}");
			Console.WriteLine($"Aujourd'hui c'est {dt.DayOfWeek}");

			Console.WriteLine(message);
		}

		public static void TesterEnumsPerso()
		{
			
			
			// Affiche le libellé défini dans l'attribut Description d'une valeur énumérée
			Feux f = Feux.Green;
			Console.WriteLine(f.GetDescription());
		}
	}
}
