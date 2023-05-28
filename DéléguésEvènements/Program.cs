using System.Data;

namespace DéléguésEvènements
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TesterParamètreFonction();
			//TesterVisitesVéto();
			//TesterExpresionsLambda();
			//TesterEvènement();
		}

		static void TesterParamètreFonction()
		{
			List<string> noms = new() { "Pierre", "Léa", "Chloé", "Paul", "Eric" };

			List<string> liste = noms.FindAll(CommenceParP);

			foreach (string n in liste)
			{
				Console.WriteLine(n);
			}
		}

		static bool CommenceParP(string s)
		{
			return s.StartsWith('P');
		}

		static void TesterVisitesVéto()
		{
			Animal ani1 = new Animal("Zapette");
			ani1.EnregistrerVisiteVéto(ActeVétérinaire.Tatouer, DateTime.Today.AddMonths(-6));
			ani1.EnregistrerVisiteVéto(ActeVétérinaire.Déparasiter, DateTime.Today.AddMonths(-3));
			ani1.EnregistrerVisiteVéto((a, d) => a.CarnetSanté.Add(new ActeVétérinaire(d, $"Vaccination contre la rage")), DateTime.Today);

			foreach (var acte in ani1.CarnetSanté)
			{
				Console.WriteLine($"{acte.Date:d} : {acte.Description}");
			}

			// Regroupe des actes sur une variable de type délégué
			DéléguéActesVéto visiteContrôle = ActeVétérinaire.Peser;
			visiteContrôle += ActeVétérinaire.Déparasiter;

			// Réalise tous ces ates en une seule fois
			Animal ani2 = new Animal("Brutus");
			ani2.EnregistrerVisiteVéto(visiteContrôle, DateTime.Today);

			Console.WriteLine();
			foreach (var acte in ani2.CarnetSanté)
			{
				Console.WriteLine($"{acte.Date:d} : {acte.Description}");
			}
		}

		static void TesterExpresionsLambda()
		{
			List<string> noms = new() { "Pierre", "Léa", "Chloé", "Paul", "Eric" };

			// Syntaxe delegate vs syntaxe expression lambda
			List<string> liste1 = noms.FindAll(delegate (string n) { return n.StartsWith('P'); });
			List<string> liste2 = noms.FindAll(n => n.StartsWith('P'));

			// Plusieurs paramètres
			var req1 = noms.Where((string n, int i) => n.StartsWith('P') && i > 1);
			foreach (string n in req1) Console.WriteLine(n);

			// Paramètres inutilisés référencés par _
			var req2 = noms.Where((n, _) => n.StartsWith('P'));

			// Aucun paramètre
			Action[] actions = new Action[2];
			actions[0] = () => Console.WriteLine("Bonjour");
			actions[1] = () => Environment.Exit(0);

			foreach (Action act in actions) act();

			// Corps avec plusieurs instructions
			List<string> liste3 = new(), liste4 = new();
			noms.ForEach(n =>
			{
				if (n.StartsWith('P'))
					liste3.Add(n);
				else
					liste4.Add(n);
			});

			Console.WriteLine();
			foreach (string n in liste3) Console.WriteLine(n);
			Console.WriteLine();
			foreach (string n in liste4) Console.WriteLine(n);

			// Retour de type tuple
			var req3 = noms.Select(n => (n.Length, n.StartsWith('P')));
			foreach ((int, bool) t in req3) Console.WriteLine(t);
		}

		static void TesterEvènement()
		{
			int nbDos = 0, nbFicTotal = 0;
			double tailleTotale = 0;

			// On réagit aux événements émis par l'analyseur en ajoutant 
			// un gestionnaire sous forme d'expression lambda
			AnalyseurDossier.EventDossierTrouvé += (object? sender, StatsDossier e) =>
			{
				// Affiche les stats du dosier parcouru
				Console.WriteLine($"{e.Chemin} : {e.NbFichiers} fichiers, {e.Taille:N0} Ko");

				// Incrémente les compteurs globaux
				nbDos++;
				nbFicTotal += e.NbFichiers;
				tailleTotale += e.Taille;
			};

			// On lance l'analyse du dossier Program Files
			string dossier = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			AnalyseurDossier.AnalyserDossier(dossier);

			// On affiche les totaux
			Console.WriteLine($"Total : {nbDos} dossiers, {nbFicTotal} fichiers, {tailleTotale:N0} Ko");
		}
	}
}