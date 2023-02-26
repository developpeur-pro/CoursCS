using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Enumérations
{
	public enum Tailles { XS, S, M, L, XL, XXL }
	public enum TaillesChemises { XS = 36, S = 37, M = 39, L = 41, XL = 43, XXL = 45 }

	[Flags]
	public enum Droits
	{
		Aucun = 0,
		Lecture = 1,
		Création = 2,
		Exécution = 4,
		Modification = 8
	}

	internal static class DemoEnumsPerso
	{
		public static void TesterInterprétation()
		{
			Tailles taille = Tailles.M;
			bool repOk = false;
			while (!repOk)
			{
				Console.WriteLine("Quelle est votre taille (XS -> XXL) ?");
				string? rep = Console.ReadLine();
				repOk = Enum.TryParse(rep, out taille) && Enum.IsDefined(taille);
			}

			Console.WriteLine(taille);
		}

		public static void TesterInterprétationEntier(int n)
		{
			if (Enum.IsDefined(typeof(Tailles), n))
			{
				Tailles t = (Tailles)n;
				Console.WriteLine(t);
			}
			else
			{
				Console.WriteLine($"{n} ne correspond à aucune valeur énumérée");
			}
		}

		public static void TesterConversion()
		{
			for (int i = 36; i < 47; i++)
			{
				TaillesChemises t = DemoEnumsPerso.ConvertirTaille(i);
				Console.WriteLine($"{i} : {t}");
			}
		}

		/// <summary>
		/// Convertit une taille de chemise française en taille américaine
		/// </summary>
		/// <param name="tf">taille française</param>
		/// <returns>taille américaine</returns>
		public static TaillesChemises ConvertirTaille(int tf)
		{
			TaillesChemises res = TaillesChemises.XS;
			foreach (TaillesChemises taille in Enum.GetValues(typeof(TaillesChemises)))
			{
				if ((int)taille <= tf)
					res = taille;
				else
					break;
			}

			return res;
		}

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
