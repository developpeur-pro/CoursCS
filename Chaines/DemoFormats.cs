using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaines
{
	internal class DemoFormats
	{
		public static void TesterFormatsNombres()
		{
			//------------------------------------------------------------
			Console.WriteLine("\nFormats de nombres prédéfinis N et C");

			decimal prix = 3.5m;

			Console.WriteLine(prix.ToString());	// 3,5

			Console.WriteLine(prix.ToString("N3")); // 3,500

			Console.WriteLine(prix.ToString("C2")); // 3,50 €

			Console.WriteLine(prix.ToString(CultureInfo.GetCultureInfo("en-US"))); // 3.5
			
			Console.WriteLine(prix.ToString("C2", CultureInfo.GetCultureInfo("en-US"))); // $3.50

			Console.WriteLine(prix.ToString("C2", CultureInfo.InvariantCulture)); // ¤3.50

			//------------------------------------------------------------
			Console.WriteLine("\nFormats de nombres personnalisés");

			long SIREN = 12345678;
			Console.WriteLine(SIREN.ToString("000 000 000"));   // 123 456 789

			SIREN = 12345678;
			Console.WriteLine(SIREN.ToString("000 000 000"));   // 012 345 678

			long tel = 0612345678;
			Console.WriteLine(tel.ToString("+33 ## ## ## ## ##")); // +33 6 12 34 56 78

			Console.WriteLine("\nNombres de chiffres avant et après la virgule");
			Console.WriteLine(prix.ToString("00.000"));  // 03,500

			decimal taux = 0.1254m;
			Console.WriteLine("\nPourcentage");
			Console.WriteLine(taux.ToString("#.0%"));  // 12,5%
		}

		public static void TesterFormatsDates()
		{
			//------------------------------------------------------------
			Console.WriteLine("\nFormats de dates prédéfinis");

			DateTime date = new DateTime(2030, 07, 14, 13, 58, 35); // 14/07/2030 à 13:58:35

			Console.WriteLine(date.ToString("d")); // 14/07/30

			Console.WriteLine(date.ToString("D")); // dimanche 14 juillet 2030

			Console.WriteLine(date.ToString("f")); // dimanche 14 juillet 2030 13:58

			Console.WriteLine(date.ToString("M")); // 14 juillet

			Console.WriteLine(date.ToString("Y")); // juillet 2030

			Console.WriteLine(date.ToString("t")); // 13:58

			//------------------------------------------------------------
			Console.WriteLine("\nFormats de dates personnalisés");

			Console.WriteLine(date.ToString("ddd d MMM yyyy"));  // dim. 14 juil. 2030

			Console.WriteLine(date.ToString("dd MMM yyyy à HH:mm z"));  // 14 juil. 2030 à 13:58
		}

		public static void TestFormatsDurées()
		{
			//------------------------------------------------------------
			Console.WriteLine("\nFormats d'intervalles de temps prédéfinis");

			TimeSpan durée = new TimeSpan(40, 37, 28); // 40h, 37 min, 28s
			Console.WriteLine(durée.ToString()); // 1.16::37:28
			// NB/ Le nombre de jours ne s'affiche que quand le nombre d'heures dépasse 24

			Console.WriteLine(durée.ToString("G")); // 1:16:37:28,0000000
			// Le nombre de jours s'affiche toujours, ainsi que le nombre de 1/10 de µs

			//------------------------------------------------------------
			Console.WriteLine("\nFormats d'intervalles de temps personnalisés");
			Console.WriteLine(durée.ToString("d'j 'hh'h 'mm'min 'ss's'")); // 1j 16h 37min 28s

			// Contrairement aux formats de dates,
			// il faut mettre les chaînes non interprétées entre ' '
		}
	}
}
