using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaines
{
	internal class DemoInterpolation
	{
		public static void TesterInterpolation()
		{
			string nom = "Paul";
			decimal somme = 13000000;
			DateTime date = new DateTime(2020, 3, 13);

			// Chaîne interpolée simple
			Console.WriteLine($"{nom} a gagné {somme} au loto le {date}");
			
			// Chaîne interpolée avec formats
			Console.WriteLine($"{nom} a gagné {somme:C0} au loto le {date:ddd d MMM yyyy}");

			// Chaîne de format composite dans string.Format
			string res = string.Format("{0,20} a gagné {1:C0} au loto le {2:D}", nom, somme, date);
			Console.WriteLine(res);

			// Chaîne de format composite placée directement dans Console.WriteLine
			Console.WriteLine("{0,20} a gagné {1:C0} au loto le {2:D}", nom, somme, date);

			// Alignement et nombre de caratères d'affichage
			string produit = "Chocolat";
			int quantité = 2;
			decimal PU = 2.15m;

			string s = $"""
				Produit          | Qté |       PU |
				-----------------+-----+----------+
				{produit,-16} | {quantité,3:N0} | {PU,8:C2} |
				""";

			Console.WriteLine(s);
		}
	}
}
