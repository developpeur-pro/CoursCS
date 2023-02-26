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
			Console.WriteLine(message);
			Console.WriteLine();
		}

		public static void TesterItérationEnum()
		{
			string[] couleurs = { "Noir", "Bleu foncé", "Vert foncé", "Cyan foncé", "Rouge foncé", "Magenta foncé", "Jaune foncé",
						"Gris", "Gris foncé", "Bleu", "Vert", "Cyan", "Rouge", "Magenta", "Jaune", "Blanc"};

			// Mémorisation de la couleur de police d'origine
			ConsoleColor couleurOrig = Console.ForegroundColor;

			// Parcours des couleurs possibles
			for (ConsoleColor c = ConsoleColor.Black; c <= ConsoleColor.White; c++)
			{
				Console.ForegroundColor = c;
				Console.WriteLine($"  {c:D} : {c}");
			}

			foreach (ConsoleColor c in Enum.GetValues(typeof(ConsoleColor)))
			{
				Console.ForegroundColor = c;
				Console.WriteLine($"  {c:D} : {couleurs[((int)c)]}");
			}

			// Restauration de la couleur d'origine
			Console.ForegroundColor = couleurOrig;
		}
	}
}
