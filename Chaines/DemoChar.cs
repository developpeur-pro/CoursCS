using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaines
{
	internal class DemoChar
	{
		// Méthodes statiques
		public static void TesterMéthodesStatiques()
		{
			string s = """
				Au 30 juin 2022, en France, 51 % des abonnements internet à haut et très haut débit
				étaient en fibre optique (+ 11 points en un an).
				""";

			int nbLettres = 0, nbChiffres = 0, nbPonctuations = 0, nbEspaces = 0, nbSymboles = 0;
			int nbMajuscules=0;
			foreach (char c in s.ToCharArray())
			{
				if (char.IsLetter(c))
					nbLettres++;
				else if (char.IsDigit(c))
					nbChiffres++;
				else if (char.IsPunctuation(c))
					nbPonctuations++;
				else if (char.IsWhiteSpace(c))
					nbEspaces++;
				else if (char.IsSymbol(c))
					nbSymboles++;
				
				if (char.IsUpper(c)) nbMajuscules++;
			}

			string res = $"""
				{s.Length} caractères, dont :
				- {nbLettres} lettres, dont {nbMajuscules} majuscules
				- {nbChiffres} chiffres
				- {nbPonctuations} ponctuations
				- {nbEspaces} espaces
				- {nbSymboles} symboles
				""";

			Console.WriteLine(res);
		}
	}
}
