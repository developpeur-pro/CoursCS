namespace DéléguésEvènements
{
	public class AnalyseurDossier
	{
		// Déclaration de l'événement
		public static event EventHandler<StatsDossier>? EventDossierTrouvé;

		// Méthode récursive d'analyse d'un dossier
		public static void AnalyserDossier(string chemin)
		{
			DirectoryInfo dossier = new(chemin);
			if (!dossier.Exists) return;

			// Récupère la liste des fichiers du dossier et mémorise ses stats
			FileInfo[] fichiers = dossier.GetFiles();
			StatsDossier stats = new(chemin, fichiers.Length, fichiers.Sum(f => f.Length) / 1024d);

			// Notifie le parcours du dossier et renvoie ses stats
			EventDossierTrouvé?.Invoke(null, stats);

			// Parcourt les sous-dossiers dont on a le droit de lister le contenu
			var dir = dossier.EnumerateDirectories().Where(Listable);
			foreach (DirectoryInfo di in dir)
			{
				AnalyserDossier(di.FullName);
			}
		}

		// Vérifie qu'on a le droit de lister le contenu d'un dossier
		private static bool Listable(DirectoryInfo di)
		{
			try
			{
				di.EnumerateFiles();
				return true;
			}
			catch (UnauthorizedAccessException)
			{
				return false;
			}
		}
	}

	// Rassemble le nombre de fichiers et la taille totale d'un dossier
	public record class StatsDossier(string Chemin, int NbFichiers, double Taille);
}
