using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumérations
{
	internal class DemoEnumsFlags
	{
		[Flags] public enum Droits	{ Aucun = 0, Lecture = 1, Création = 2, Exécution = 4, Modification = 8	}

		public static void TesterEnumFlags()
		{
			// Création d'utilisateurs avec des droits associés
			(string nom, Droits droits)[] utilisateurs = new (string, Droits)[6];
			utilisateurs[0] = ("Yves", Droits.Aucun);
			utilisateurs[1] = ("Aline", Droits.Lecture);
			utilisateurs[2] = ("Marie", Droits.Exécution | Droits.Modification);
			utilisateurs[3] = ("Eric", Droits.Lecture | Droits.Exécution);
			utilisateurs[4] = ("Léa", Droits.Création | Droits.Modification);
			utilisateurs[5] = ("Denis", Droits.Lecture | Droits.Création | Droits.Exécution | Droits.Modification);

			Console.WriteLine("Utilisateurs ayant au moins le droit de modification : ");
			foreach ((string nom, Droits droits) in utilisateurs)
			{
				if (droits.HasFlag(Droits.Modification))
					Console.WriteLine(nom); // Affiche successivement Marie, Léa et Denis
			}

			Console.WriteLine("\nUtilisateurs ayant au moins les droits de lecture et d'exécution : ");
			foreach ((string nom, Droits droits) in utilisateurs)
			{
				if (droits.HasFlag(Droits.Lecture | Droits.Exécution))
					Console.WriteLine(nom); // Affiche successivement Eric et Denis
			}

			Console.WriteLine("\nUtilisateurs ayant au moins les droits de lecture ou d'exécution : ");
			foreach ((string nom, Droits droits) in utilisateurs)
			{
				//if (droits.HasFlag(Droits.Lecture) | droits.HasFlag(Droits.Exécution))
				if ((droits & (Droits.Lecture | Droits.Exécution)) != 0)
					Console.WriteLine(nom); // Affiche successivement Aline, Marie, Eric, Denis
			}

			// Ajout du droit de modification à Aline
			utilisateurs[1].droits |= Droits.Modification;
			Console.WriteLine("\nDroits d'Aline : " + utilisateurs[1].droits);


			// Retrait du droit d'exécution à Eric
			utilisateurs[3].droits ^= Droits.Exécution;
			Console.WriteLine("\nDroits d'Eric : " + utilisateurs[3].droits);
		}
	}
}
