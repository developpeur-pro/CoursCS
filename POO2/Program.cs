using POO2.Model;

namespace POO2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			#region Compositon et agrégation

			// Composition
			Joueur j1 = new Joueur("Riou", "Rémy");
			j1.CreerLicence(75968541, "Olympique lyonnais");

			if (j1.LicenceCourante != null)
				Console.WriteLine($"Licence N° {j1.LicenceCourante.Numero} " +
					$"délivrée le {j1.LicenceCourante.DateDelivrance}");

			// Agrégation
			Club SE = new Club("AS Saint-Etiene");
			Club OL = new Club("Olympique lyonnais");
			Rencontre r = new Rencontre(DateTimeOffset.Now, SE, OL);

			Console.WriteLine($"Rencontre entre les clubs {SE.Nom} et {OL.Nom} le {r.DateRencontre}");
			#endregion

			#region Liste générique
			// Ajout de joueurs à la liste des joueurs d'un club
			OL.Joueurs.Add(j1);
			Joueur j2 = new Joueur("Bonnevie", "Kayne");
			OL.Joueurs.Add(j2);
			Joueur j3 = new Joueur("Silva", "Henrique");
			OL.Joueurs.Add(j3);

			// Parcourt la liste 
			for (int i = 0; i < OL.Joueurs.Count; i++)
			{
				Console.WriteLine(OL.Joueurs[i].Nom);
			}

			// Ajoute un joueur en première position dans la liste
			Joueur j4 = new Joueur("Lopes", "Anthony");
			OL.Joueurs.Insert(0, j4);

			// Supprime le dernier joueur de la liste
			OL.Joueurs.RemoveAt(OL.Joueurs.Count - 1);

			Console.WriteLine();
			foreach (Joueur joueur in OL.Joueurs)
			{
				Console.WriteLine(joueur.Nom);
			}

			// Récupère tous les joueurs dont le nom commence par Bo
			List<Joueur> liste = OL.Joueurs.FindAll(j => j.Nom.StartsWith("Bo"));

			// Récupère le premier joueur dont le nom commence par Bo
			Joueur? j = OL.Joueurs.Find(j => j.Nom.StartsWith("Bo"));

			// Vérifie si un joueur est présent dans la liste
			bool present = OL.Joueurs.Contains(j3);
			Console.WriteLine($"{j3.Nom}{(present ? "" : " non")} présent dans la liste");

			#endregion
		}
	}
}