using Microsoft.Data.Sqlite;
using System;

namespace Erreurs
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Décommenter la ligne souhaitée

			TesterBanque1();
			//TesterBanque2();
			//TesterBanque2AvecErreur();
			//TesterBanque2AvecErreurSpécifiques();
			//TesterPropagation();
			//TesterRequeteBdd();
		}

		// Test de la classe Banque 1
		static void TesterBanque1()
		{
			// Enregistrement d'un nouveau client
			Client pers = new() { Nom = "Ricaud", Prénom = "Léa" };
			if (Banque1.EnregistrerClient(pers, out long numClient))
			{
				Console.WriteLine($"Client créé sous le N°{numClient}");

				// Ouverture d'un compte
				var res = Banque1.OuvrirCompte(numClient, TypesCompte.Courant, out long numCompte);
				if (res == RésultatsOuvertureCompte.OK)
				{
					Console.WriteLine($"Compte {numCompte} créé pour le client {numClient}");

					// Envoi d'une carte bancaire
					if (Banque1.EnvoyerCarte(numClient, numCompte))
					{
						Console.WriteLine($"Carte envoyée au client");
					}
					else
					{
						Console.WriteLine($"Client {numClient} ou compte courant N°{numCompte} non trouvé");
					}
				}
				else if (res == RésultatsOuvertureCompte.NumClientInvalide)
				{
					Console.WriteLine($"Client {numClient} inexistant");
				}
				else if (res == RésultatsOuvertureCompte.TypeCompteDéjàExistant)
				{
					Console.WriteLine($"Un compte de même type est déjà ouvert pour ce client");
				}
			}
			else
			{
				Console.WriteLine("Les nom et prénom du client doivent être renseignés");
			}
		}

		// Test de la classe Banque2 dans le cas ordinaire (pas d'erreur)
		static void TesterBanque2()
		{
			try
			{
				// On enregistre un nouveau client
				Client pers = new();
				long numClient = Banque2.EnregistrerClient(pers);
				Console.WriteLine($"Client créé sous le N°{numClient}");

				// On lui ouvre un compte
				long numCompte = Banque2.OuvrirCompte(numClient, TypesCompte.Courant);
				Console.WriteLine($"Compte {numCompte} créé pour le client {numClient}");

				// On lui envoie carte sa bancaire
				Banque2.EnvoyerCarte(numClient, numCompte);
				Console.WriteLine($"Carte envoyée au client");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

		}

		// Test de la classe Banque2 avec erreur et interception via le type Exception
		static void TesterBanque2AvecErreur()
		{
			try
			{
				// On enregistre un nouveau client
				Client pers = new() { Nom = "Ricaud", Prénom = "Léa" };
				long numClient = Banque2.EnregistrerClient(pers);
				Console.WriteLine($"Client créé sous le N°{numClient}");

				// On lui ouvre un compte courant
				long numCompte = Banque2.OuvrirCompte(numClient, TypesCompte.Courant);
				Console.WriteLine($"Compte {numCompte} créé pour le client {numClient}");

				// On lui ouvre un autre compte courant
				long numCompte2 = Banque2.OuvrirCompte(numClient, TypesCompte.Courant);
				Console.WriteLine($"Compte {numCompte2} créé pour le client {numClient}");
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("Fin du test");
		}

		// Test de la classe Banque2 avec erreur et interception via des types d'exceptions spécifiques
		static void TesterBanque2AvecErreurSpécifiques()
		{
			try
			{
				// On enregistre un nouveau client
				Client pers = new() { Nom = "Ricaud", Prénom = "Léa" };
				long numClient = Banque2.EnregistrerClient(pers);
				Console.WriteLine($"Client créé sous le N°{numClient}");

				// On lui ouvre un compte courant
				long numCompte = Banque2.OuvrirCompte(numClient, TypesCompte.Courant);
				Console.WriteLine($"Compte {numCompte} créé pour le client {numClient}");

				// On lui ouvre un autre compte courant
				long numCompte2 = Banque2.OuvrirCompte(numClient, TypesCompte.Courant);
				Console.WriteLine($"Compte {numCompte2} créé pour le client {numClient}");
			}
			catch (InvalidOperationException e)
			{
				if (e.Data.Contains("numCompte"))
				{
					string msg = "Un compte de même type a déjà été créé pour ce client";
					long? numCompte = (long?)e.Data["numCompte"];
					if (numCompte != null)
						msg += $" (compte N°{numCompte})";

					Console.WriteLine(msg);
				}
				else
					Console.WriteLine(e.Message);
			}
			catch (Exception e) // Interception de tous les autres types d'erreurs
			{
				Console.WriteLine(e.Message);
			}

			Console.WriteLine("Fin du test");
		}

		#region Propagation des exceptions
		static void TesterPropagation()
		{
			try
			{
				MethodeNiveau1();
			}
			catch (Exception)
			{
				Console.WriteLine("Interception de l'erreur");
			}
		}

		static void MethodeNiveau1()
		{
			MethodeNiveau2();
		}

		static void MethodeNiveau2()
		{
			try
			{
				MethodeNiveau3();

			}
			catch (NotImplementedException)
			{
				Console.WriteLine("Gestion dans le niveau 2");
				throw;
			}
		}

		static void MethodeNiveau3()
		{
			throw new NotImplementedException(); // lève une exception
		}
		#endregion

		#region using et finally
		// Test des requêtes sur une base SQLite
		static void TesterRequeteBdd()
		{
			try
			{
				//long nbClients = GetNbClients();	// avec try catch classique
				long nbClients = GetNbClients1(); // avec using
				//long nbClients = GetNbClients2(); // avec try finally

				Console.WriteLine($"{nbClients} clients dans la banque");
			}
			catch (SqliteException e)
			{
				Console.WriteLine("Erreur lors de la requête : " + e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Autre erreur : " + e.Message);
			}
		}

		// Récupère le nombre de clients depuis une base SQLite
		public static long GetNbClients()
		{
			// Créer une connexion à une base SQLite existante
			string cs = "Data Source=Banque.db; Mode=ReadWrite";
			var connect = new SqliteConnection(cs);
			long? nbClients = null;
			try
			{
				// Ouvre la connexion
				connect.Open();

				// Exécute une commande sur la base
				string req = @"select count(*) from Clients";
				SqliteCommand cmd = new SqliteCommand(req, connect);
				nbClients = (long?)cmd.ExecuteScalar();

				// Ferme la connexion
				connect.Close();
			}
			catch (Exception)
			{
				connect.Close();
			}

			return nbClients ?? 0;
		}

		// Récupère le nombre de clients depuis une base SQLite
		public static long GetNbClients1()
		{
			// Créer une connexion à une base SQLite existante
			string cs = "Data Source=Banque.db; Mode=ReadWrite";
			using var connect = new SqliteConnection(cs);

			// Ouvre la connexion
			connect.Open();

			// Exécute une commande sur la base
			string req = @"select count(*) from Clients";
			SqliteCommand cmd = new SqliteCommand(req, connect);
			long? nbClients = (long?)cmd.ExecuteScalar();

			return nbClients ?? 0;
			// Grâce au using, la connexion est automatiquement fermée ici
		}

		// Méthode équivalente en utilisant l'instruction try finally
		public static long GetNbClients2()
		{
			string cs = "Data Source=Banque.db; Mode=ReadWrite";
			var connect = new SqliteConnection(cs);

			long? nbClients;
			try
			{
				// Ouvre la connexion
				connect.Open();

				// Exécute une commande sur la base
				string req = @"select count(*) from _Clients";
				SqliteCommand cmd = new SqliteCommand(req, connect);
				nbClients = (long?)cmd.ExecuteScalar();
			}
			finally
			{
				// Ferme explicitement la connexion
				connect.Close();
			}

			return nbClients ?? 0;
		}
		#endregion
	}
}
