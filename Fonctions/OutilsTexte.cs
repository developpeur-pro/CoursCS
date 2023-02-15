using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fonctions
{
	internal class OutilsTexte
	{
		public static int CompterMots(string phrase)
		{
			int nbMots = 0;
			for (int i = 0; i < phrase.Length; i++)
			{
				// On compte simplement les espaces, les apostrophes et les tabulations
				if (phrase[i] == ' ' || phrase[i] == '\'' || phrase[i] == '\t')
					nbMots++;
			}
			return nbMots++;
		}

		public static string Traduire(string texte, string langue = "EN")
		{
			return string.Empty;
		}
	}
}
