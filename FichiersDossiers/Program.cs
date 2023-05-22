using System.Diagnostics;

namespace FichiersDossiers
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Obtient le chemin du dossier Mes documents :
			string dossierDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			// Construit un chemin par combinaison de noms de dossiers
			string dossierTravail = Path.Combine(dossierDocs, "Travail");

			// Crée le dossier correspondant à ce chemin s'il n'existe pas déjà
			Directory.CreateDirectory(dossierTravail);

			// Définit le dossier comme dossier de travail de l'application
			Directory.SetCurrentDirectory(dossierTravail);

			// Crée une arborescence de 3 sous-dossiers à l'intérieur du dossier de travail
			Directory.CreateDirectory(Path.Combine("Niveau1", "Niveau2", "Niveau3"));

			// Si le dossier Niveau1 existe...
			if (Directory.Exists("Niveau1"))
			{
				// On le renomme
				Directory.Move("Niveau1", "Niveau_1");

				// On y copie un fichier
				File.Copy("Lac.jpg", Path.Combine("Niveau_1", "Lac2.jpg"));

				// On supprime le dossier et tout son contenu
				Directory.Delete("niveau_1", true);
			}

			Console.WriteLine("Extraction des infos d'un chemin :");
			ExtraireInfosChemin(Path.Combine(dossierTravail, "Lac.jpg"));

			string chemin = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
				"Vintage");

			Console.WriteLine("\nParcours récursif des fichiers d'un dossier avec DirectoryInfo :");
			Listerfichiers(chemin);

			Console.WriteLine("\nParcours récursif des fichiers d'un dossier avec Directory :");
			ListerFichiers2(chemin);

			#region Ecriture
			Console.WriteLine("\nEcriture d'un fichier CSV");
			Ecriture.EcrireListeCSV1(chemin);
			Ecriture.EcrireListeCSV2(chemin);

			Console.WriteLine("\nEcriture d'un fichier JSON");

			Stopwatch sw = new();
			sw.Start();
			Ecriture.EcrireListeJson1(chemin);
			sw.Stop();
			Console.WriteLine($"Temps d'exécution : {sw.ElapsedTicks}");
			sw.Reset();

			sw.Start();
			Ecriture.EcrireListeJson2(chemin);
			sw.Stop();
			Console.WriteLine($"Temps d'exécution : {sw.ElapsedTicks}");
			sw.Reset();
			#endregion

			#region Lecture
			Console.WriteLine("\nLecture d'un fichier CSV");
			var liste = Lecture.LireFichierCSV1(Path.Combine(chemin, "ListeFichiers.csv"));
			var liste2 = Lecture.LireFichierCSV2(Path.Combine(chemin, "ListeFichiers.csv"));
			Console.WriteLine($"{liste.Count} enregistrements chargés");

			Console.WriteLine("\nLecture d'un fichier JSON");
			sw.Start();
			Lecture.LireFichierJSON(Path.Combine(chemin, "ListeImages.json"));
			sw.Stop();
			Console.WriteLine($"Temps d'exécution : {sw.ElapsedTicks}");
			sw.Reset();

			sw.Start();
			Lecture.LireFichierJSON2(Path.Combine(chemin, "ListeImages.json"));
			sw.Stop();
			Console.WriteLine($"Temps d'exécution : {sw.ElapsedTicks}");
			#endregion
		}

		static void ExtraireInfosChemin(string chemin)
		{
			Console.WriteLine($"""
				Chemin complet : {chemin}
				Dossier : {Path.GetDirectoryName(chemin)}
				Nom du fichier avec extension : {Path.GetFileName(chemin)}
				Nom du fichier sans extension : {Path.GetFileNameWithoutExtension(chemin)}
				Extension : {Path.GetExtension(chemin)}
				Changement d'extension : {Path.ChangeExtension(chemin, ".png")}
				""");
		}

		static void Listerfichiers(string cheminDossier)
		{
			DirectoryInfo di = new(cheminDossier);
			if (!di.Exists) return;

			string[] exts = { ".png", ".jpg", ".webp" };

			// Enumère les fichiers images présents dans le dossier et ses sous-dossiers
			var images = from fi in di.EnumerateFiles("*.*", SearchOption.AllDirectories)
							 where exts.Contains(fi.Extension)
							 select fi;

			// Affiche les caractéristiques des fichiers
			foreach (FileInfo fi in images)
			{
				Console.WriteLine($"{fi.Name}, {fi.CreationTime}, {fi.Length / 1024} Ko");
			}
		}

		static void ListerFichiers2(string cheminDossier)
		{
			if (!Directory.Exists(cheminDossier)) return;

			string[] exts = { ".png", ".jpg", ".webp" };

			// Enumère les chemins des fichiers images présents
			// dans le dossier et ses sous-dossiers
			var chemins = from c in Directory.EnumerateFiles(cheminDossier,
									"*.*", SearchOption.AllDirectories)
							  where exts.Contains(Path.GetExtension(c))
							  select c;

			// Affiche les caractéristiques des fichiers
			foreach (string c in chemins)
			{
				FileInfo fi = new FileInfo(c);
				Console.WriteLine($"{fi.Name}, {fi.CreationTime}, {fi.Length / 1024} Ko");
			}
		}
	}
}