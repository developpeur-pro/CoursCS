using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO2.Model
{
	internal class Joueur
	{
		public string Nom { get; }
		public string Prenom { get; }
		public Licence? LicenceCourante { get; private set; }

		public Joueur(string nom, string prenom)
		{
			Nom = nom;
			Prenom = prenom;
		}

		public void CreerLicence(long numero, string club)
		{
			DateOnly dateDeliv = DateOnly.FromDateTime(DateTime.Today);
			LicenceCourante = new Licence(numero, club, dateDeliv);
		}
	}

	internal class Licence
	{
		public long Numero { get; }
		public DateOnly DateDelivrance { get; }
		public string NomClub { get; set; }

		public Licence(long numero, string nomClub, DateOnly dateDelivrance)
		{
			Numero = numero;
			NomClub = nomClub;
			DateDelivrance = dateDelivrance;
		}
	}
}
