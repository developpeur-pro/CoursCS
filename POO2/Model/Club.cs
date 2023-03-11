using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO2.Model
{
	internal class Rencontre
	{
		public DateTimeOffset DateRencontre { get; }
		public Club Club1 { get; }
		public Club Club2 { get; }

		public Rencontre(DateTimeOffset dateRencontre, Club club1, Club club2)
		{
			DateRencontre = dateRencontre;
			// Agrégation de clubs créés en dehors de la classe Rencontre
			Club1 = club1;
			Club2 = club2;
		}
	}

	internal class Club
	{
		public string Nom { get; }
		public string Ville { get; set; } = string.Empty;
		public List<Joueur> Joueurs { get; }

		public Club(string nom)
		{
			Nom = nom;
			Joueurs = new List<Joueur>();
		}
	}
}
