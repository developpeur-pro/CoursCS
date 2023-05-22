using System.Globalization;
using System.Text.Json;

namespace FichiersDossiers
{
	public class Lecture
	{
		public static List<InfosFichier> LireFichierCSV1(string chemin)
		{
			List<InfosFichier> fichiers = new();
			InfosFichier fichier;
			string? ligne;

			using StreamReader reader = new(chemin);
			reader.ReadLine(); // Lit la ligne d'en-têtes sans la traiter
			while ((ligne = reader.ReadLine()) != null)
			{
				string[] infos = ligne.Split(';');
				fichier = new InfosFichier(infos[0],
							DateTime.Parse(infos[1], CultureInfo.InvariantCulture),
							long.Parse(infos[2]));

				fichiers.Add(fichier);
			}

			return fichiers;
		}

		public static List<InfosFichier> LireFichierCSV2(string chemin)
		{
			List<InfosFichier> fichiers = new();
			InfosFichier fichier;
			string? ligne;

			// Récupère le contenu complet du fichier sous forme de chaîne
			string txt = File.ReadAllText(chemin);

			using StringReader reader = new(txt);
			reader.ReadLine(); // Lit la ligne d'en-têtes sans la traiter
									 // Lit les lignes suivantes
			while ((ligne = reader.ReadLine()) != null)
			{
				string[] infos = ligne.Split(';');
				fichier = new InfosFichier(infos[0],
							DateTime.Parse(infos[1], CultureInfo.InvariantCulture),
							long.Parse(infos[2]));

				fichiers.Add(fichier);
			}

			return fichiers;
		}

		public static List<InfosFichier> LireFichierJSON(string chemin)
		{
			using FileStream fs = File.OpenRead(chemin);
			var fichiers = JsonSerializer.Deserialize<List<InfosFichier>>(fs);

			return fichiers ?? new List<InfosFichier>();
		}

		public static List<InfosFichier> LireFichierJSON2(string chemin)
		{
			// Récupère le contenu complet du fichier sous forme de chaîne
			string txt = File.ReadAllText(chemin);

			// Désérialise ce contenu dans une liste d'objets
			var fichiers = JsonSerializer.Deserialize<List<InfosFichier>>(txt);

			return fichiers ?? new List<InfosFichier>();
		}

	}
}
