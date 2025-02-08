using SixLabors.ImageSharp;
using System.Net.NetworkInformation;

namespace ProgAsychrone
{
	class Program
	{
		// Générateur de jetons d'annulation de tâches
		private static readonly CancellationTokenSource _cts = new();

		//static void Main(string[] args)
		//{
		//LancerMethodesConsecutives();
		//LancerTachesParallelesAsync().Wait();
		//}

		static async Task Main(string[] args)
		{
			await LancerTachesParallelesAsync();
			//await LancerTachesParalleles2Async();
			//await TesterDomaineAsync("stackoverflow");
			//await LancerTacheAutreThreadAsync();
			//await LancerTacheDuréeMaxiAsync();
			//await LancerTacheAnnulableAsync();
		}

		static void LancerMethodesConsecutives()
		{
			HeuresSoleil? heuresParis = WebAPI.GetHeuresSoleil(48.8534, 2.3486, 1);
			Console.WriteLine($"\nRésultat 1 : {heuresParis}");

			HeuresSoleil? heuresBordeaux = WebAPI.GetHeuresSoleil(44.84044, -0.5805, 2);
			Console.WriteLine($"\nRésultat 2 : {heuresBordeaux}");
		}

		// Lance 2 tâches en parallèle
		static async Task LancerTachesParallelesAsync()
		{
			// Lance 2 tâches en parallèle
			Task<HeuresSoleil?> t1 = WebAPI.GetHeuresSoleilAsync(48.8534, 2.3486, 1);
			Task<HeuresSoleil?> t2 = WebAPI.GetHeuresSoleilAsync(44.84044, -0.5805, 2);

			// Attend la fin de la première pour afficher son résultat
			HeuresSoleil? heuresParis = await t1;
			Console.WriteLine($"\nRésultat 1 : {heuresParis}");

			// Attend la fin de la seconde pour afficher son résultat
			HeuresSoleil? heuresBordeaux = await t2;
			Console.WriteLine($"\nRésultat 2 : {heuresBordeaux}");

			Console.WriteLine();
		}

		// Lance 3 tâches en parallèle et affiche leurs résultats au fur et à mesure
		// qu'elles se terminent, en gérant toutes les erreurs
		static async Task LancerTachesParalleles2Async()
		{
			// Lance 3 tâches en parallèle et leur associe un numéro
			Dictionary<Task<HeuresSoleil?>, int> taches = new();
			//taches.Add(WebAPI.GetHeuresSoleilAsync(48.8534, 2.3486, 1), 1);
			taches.Add(WebAPI.GetHeuresSoleilAsync(1000, 0, 2), 2); // cette tâche provoque une erreur
																					  //taches.Add(WebAPI.GetHeuresSoleilAsync(44.84044, -0.5805, 3), 3);

			while (taches.Any())
			{
				// Crée une tâche qui se termine dès que l'une des tâches de la liste se termine
				// et qui renvoie cette tâche terminée
				Task<HeuresSoleil?> tacheFinie = await Task.WhenAny(taches.Keys);

				// Récupère le N° associé à cette tâche
				int numTache = taches.GetValueOrDefault(tacheFinie);
				try
				{
					// Affiche le résultat de la tâche terminée
					Console.WriteLine($"\nRésultat tâche {numTache} : {tacheFinie.Result}");
				}
				catch (AggregateException ae)
				{
					// Affiche le détail des erreurs de la tâche terminée
					foreach (Exception e in ae.InnerExceptions)
					{
						Console.WriteLine($"\nErreur dans la requête {numTache} : {e.Message}");
					}
				}

				// Supprime la tâche terminée de la liste
				taches.Remove(tacheFinie);
			}
		}

		// Attend la fin d'un ensemble de tâches
		static async Task TesterDomaineAsync(string domaine)
		{
			// Crée et lance des tâches pour tester les urls
			List<Task> taches = new();
			string[] extensions = { ".com", ".fr", ".net" };
			foreach (string ext in extensions)
			{
				taches.Add(TesterUrlAsync(domaine + ext));
			}

			// Crée une tâche qui se termine quand toutes les tâches de la liste sont terminées
			Task ensemble = Task.WhenAll(taches);
			try
			{
				await ensemble;
				Console.WriteLine("Domaine non dispo pour les extensions souhaitées");
			}
			catch (PingException)
			{
				// Récupère le nombre de pings échoués
				int nbDispos = taches.Count(t => t.Status == TaskStatus.Faulted);

				if (nbDispos == extensions.Length)
					Console.WriteLine("Domaine dispo");
				else
					Console.WriteLine($"Seulement {nbDispos} extension(s) disponible(s)");
			}

			// Pour info, affiche le statut de l'ensemble de tâches
			Console.WriteLine("Statut de l'ensemble de tâches : " + ensemble.Status);
		}

		// Teste si une url répond à un ping
		static async Task TesterUrlAsync(string url)
		{
			Ping ping = new();
			await ping.SendPingAsync(url);
			// Cette ligne lève une PingException si l'url n'existe pas
		}

		// Lance un traitement dans un thread séparé
		static async Task LancerTacheAutreThreadAsync()
		{
			// Chemin d'une image très volumineuse
			string chemin = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
				 "Univers", "Ghost Nebula IC 63.tif");

			// On lance l'encodage de l'image via une tâche exécutée dans un thread séparé
			// car une exécution clasique de la méthode EncoderImage accaparerait le thread principal
			// et ne permettrait pas de garder l'UI réactive pendant ce temps
			Task<string> encodage = Task.Run(() => EncoderImage(chemin));

			// Pendant ce temps, on peut continuer à faire d'autres choses dans le thread principal
			for (int i = 0; i < 200; i++)
			{
				Console.Write('.'); // Affiche un point
				Thread.Sleep(50); // Atend 50 ms
			}

			// On attend la fin de la tâche et on récupère le chemin de l'image encodée
			string cheminRes = await encodage;

			// On affiche la taille de l'image encodée
			FileInfo fi = new(cheminRes);
			Console.WriteLine($"\nTaille de l'image encodée : {fi.Length / 1024} Ko");
		}

		// Encode et enregistre l'image passée en paramètre en webp
		static string EncoderImage(string chemin)
		{
			string cheminImageWebp = Path.ChangeExtension(chemin, "webp");
			Image img = Image.Load(chemin);
			img.SaveAsWebp(cheminImageWebp);
			Console.Write(" encodage terminé ");
			return cheminImageWebp;
		}

		// Lance une tâche avec un délai maxi d'exécution
		static async Task LancerTacheDuréeMaxiAsync()
		{
			// Programme l'activation du jeton d'annulation après un délai de 2s
			_cts.CancelAfter(2000);

			try
			{
				// Exécute une tâche
				await WebAPI.GetHeuresSoleilAsync(48.8534, 2.3486, 1, _cts.Token);
				Console.WriteLine("\nTâche terminée");
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("\nTache annulée");
			}
		}

		// Lance une tâche annulable par l'utilisateur
		static async Task LancerTacheAnnulableAsync()
		{
			// Donne à l'utilisateur la possibilité d'annuler des tâches en appuyant sur Echap
			_ = Task.Run(() =>
			{
				Console.WriteLine("\nAppuyer sur Echap pour annuler\n");
				while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
				_cts.Cancel();
			});

			try
			{
				// Lance une tâche et attend qu'elle se termine
				await WebAPI.GetHeuresSoleilAsync(48.8534, 2.3486, 1, _cts.Token);
				Console.WriteLine("\nTâche terminée avec succès");
			}
			catch (TaskCanceledException e)
			{
				Console.WriteLine($"\nTâche annulée (statut {e.Task?.Status})");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
