using System.Globalization;
using System.Text;
using System.Text.Json;

namespace FichiersDossiers
{
	internal class Ecriture
	{
		// Ecrit la liste des fichiers d'un dossier dans un fichier ListeFichers.csv
		// au moyen d'un StreamWriter
		public static void EcrireListeCSV1(string cheminDossier)
		{
			DirectoryInfo di = new(cheminDossier);
			if (!di.Exists) return;

			string chemin = Path.Combine(cheminDossier, "ListeFichiers.csv");
			using var writer = new StreamWriter(chemin, false, Encoding.UTF8);

			writer.WriteLine("Nom;DateCreation;TailleKo");
			foreach (FileInfo fi in di.EnumerateFiles())
			{
				writer.Write(fi.Name);
				writer.Write(";" + fi.CreationTime.ToString(CultureInfo.InvariantCulture) + ";");
				writer.WriteLine(fi.Length / 1024);
			}
		}
		
		// Ecrit la liste des fichiers d'un dossier dans un fichier ListeFichers.csv
		// au moyen des classes StringWriter et File
		public static void EcrireListeCSV2(string cheminDossier)
		{
			DirectoryInfo di = new(cheminDossier);
			if (!di.Exists) return;

			using var writer = new StringWriter();

			// Ecrit la ligne d'en-tête
			writer.WriteLine("Nom;DateCreation;TailleKo");

			// Ecrit la liste ligne à ligne
			foreach (FileInfo fi in di.EnumerateFiles())
			{
				writer.Write(fi.Name);
				writer.Write(";" + fi.CreationTime.ToString(CultureInfo.InvariantCulture) + ";");
				writer.WriteLine(fi.Length / 1024);
			}

			// Enregistre le texte complet dans un fichier
			string chemin = Path.Combine(cheminDossier, "ListeFichiers.csv");
			File.WriteAllText(chemin, writer.ToString());
		}

		// Ecrit la liste des images d'un dossier dans un fichier ListeImages.json
		public static void EcrireListeJson1(string cheminDossier)
		{
			DirectoryInfo di = new(cheminDossier);
			if (!di.Exists) return;

			string[] exts = { ".png", ".jpg", ".webp" };

			var images = from fi in di.EnumerateFiles("*.*", SearchOption.AllDirectories)
							 where exts.Contains(fi.Extension)
							 select new InfosFichier(fi.Name, fi.CreationTime, fi.Length / 1024);

			// NB/ Les objets FileInfo ne sont pas sérialisables tels quels
			// C'est pourquoi on les transforme en records faciles à sérialiser

			using FileStream fs = File.Create(Path.Combine(cheminDossier, "ListeImages.json"));
			var options = new JsonSerializerOptions { WriteIndented = true };
			JsonSerializer.Serialize(fs, images, options);
		}

		// Ecrit la liste des images d'un dossier dans un fichier ListeImages.json
		public static void EcrireListeJson2(string cheminDossier)
		{
			DirectoryInfo di = new(cheminDossier);
			if (!di.Exists) return;

			string[] exts = { ".png", ".jpg", ".webp" };

			var images = from fi in di.EnumerateFiles("*.*", SearchOption.AllDirectories)
							 where exts.Contains(fi.Extension)
							 select new InfosFichier(fi.Name, fi.CreationTime, fi.Length / 1024);

			var options = new JsonSerializerOptions { WriteIndented = true };
			string jsonString = JsonSerializer.Serialize(images, options);

			// Enregistre le flux JSON dans un fichier
			File.WriteAllText(Path.Combine(cheminDossier, "ListeImages.json"), jsonString);
		}
	}

	public record class InfosFichier(string Nom, DateTime DateCreation, long TailleKo);
}

