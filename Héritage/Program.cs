using System.Text;

namespace Héritage
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Unicode;

			TesterClasseDerivee();
			TesterPolymorphisme();
			TesterTranstypage();
		}

		static void TesterClasseDerivee()
		{
			CompteEpargne ce = new(22222, new DateOnly(2022, 2, 2), 2000m);
			// Ceci appelle le constructeur de CompteBancaire, puis celui de CompteEpargne

			ce.Crediter(300m, MoyensTransaction.Virement, "Epargne du mois");
		}

		static void TesterPolymorphisme()
		{
			CompteBancaire[] comptes = new CompteBancaire[3];
			comptes[0] = new CompteCourant(111111, new DateOnly(2021, 1, 1), 0m);
			comptes[1] = new CompteEpargne(222222, new DateOnly(2022, 2, 2), 50m);
			comptes[2] = new CompteTitres(333333, new DateOnly(2023, 3, 3), 100m);

			// Appels polymorphiques de méthodes sur les comptes
			try
			{
				foreach (CompteBancaire compte in comptes)
				{
					compte.Crediter(500m, MoyensTransaction.Carte, "Crédit");
					compte.Debiter(500m, MoyensTransaction.Virement, "Débit");
					Console.WriteLine(compte.ToString());
				}
			}
			catch (ArgumentOutOfRangeException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void TesterTranstypage()
		{
			CompteBancaire[] comptes = new CompteBancaire[3];
			comptes[0] = new CompteCourant(111111, new DateOnly(2021, 1, 1), 1000m);
			comptes[1] = new CompteEpargne(222222, new DateOnly(2022, 2, 2), 2000m);
			comptes[2] = new CompteTitres(333333, new DateOnly(2023, 3, 3), 3000m);

			foreach (CompteBancaire compte in comptes)
			{
				// Accès aux propriétés communes
				Console.WriteLine($"{compte.Libelle} N°{compte.Numero} ouvert le {compte.DateOuverture:d}");

				// Accès aux propriétés spécifiques des différents types de comptes
				if (compte is CompteCourant cc)
				{
					Console.WriteLine($"Découvert maxi autorisé : {cc.DecouvertMaxiAutorise:C0}");
				}
				else if (compte is CompteEpargne ce)
				{
					Console.WriteLine($"Taux d'intérêts annuels : {ce.TauxInterets:#.##%}");
				}
				else if (compte is CompteTitres ct)
				{
					Console.WriteLine($"Valeurs en portefeuille : {ct.Portefeuille.Count}");
				}
				Console.WriteLine();
			}

			// Opérateur as
			CompteEpargne? c = comptes[0] as CompteEpargne;
			if (c != null)
			{
				Console.WriteLine($"Taux d'intérêts annuels : {c.TauxInterets:#.##%}");
			}
		}
	}
}