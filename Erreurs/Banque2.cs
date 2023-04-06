namespace Erreurs
{
	public class Banque2
	{
		private static List<Client> _clients = new();

		/// <summary>
		/// Enregistre un nouveau client et renvoie son numéro
		/// </summary>
		/// <param name="personne">personne sans N° de client</param>
		/// <returns>Numéro de client</returns>
		/// <exception cref="ArgumentNullException">nom ou prénom du client non renseigné</exception>
		public static long EnregistrerClient(Client personne)
		{
			// Vérifie les infos du client potentiel
			if (string.IsNullOrWhiteSpace(personne.Nom) || string.IsNullOrWhiteSpace(personne.Prénom))
				throw new ArgumentNullException("", "Les nom et prénom du client doivent être renseignés");

			// Attribue un N° au client
			Client client = new()
			{
				Numéro = long.Parse(DateTime.Now.ToString("yyMMddHHmmss")),
				Nom = personne.Nom.Trim(),
				Prénom = personne.Prénom.Trim(),
			};

			// L'enregistre dans la liste des clients
			_clients.Add(client);

			return client.Numéro;
		}

		/// <summary>
		/// Ouvre un compte du type spécifié pour le client spécifié
		/// </summary>
		/// <param name="numClient">numéro du client</param>
		/// <param name="type">type de compte à ouvrir</param>
		/// <returns>numéro du compte</returns>
		/// <exception cref="KeyNotFoundException">client non trouvé dans la liste</exception>
		/// <exception cref="InvalidOperationException">un compte de même type existe déjà pour ce client</exception>
		public static long OuvrirCompte(long numClient, TypesCompte type)
		{
			// Recherche le client dans la liste
			int indiceClient = GetIndiceClient(numClient);
			if (indiceClient == -1)
				throw new KeyNotFoundException($"Client N°{numClient} non trouvé");

			// Vérifie si un compte de même type existe déjà
			foreach (Compte compte in _clients[indiceClient].Comptes)
			{
				if (compte.TypeCompte == type)
				{
					InvalidOperationException e = new($"Un compte de même type existe déjà pour ce client");
					e.Data["numCompte"] = compte.Numero; // Transmet le N° du comtpe existant
					throw e;
				}
			}

			// Ouvre le compte
			long numCompte = long.Parse(numClient.ToString() + (int)type);
			_clients[indiceClient].Comptes.Add(new Compte(numCompte, type));

			return numCompte;
		}

		/// <summary>
		/// Envoie la carte associée au compte courant du client spécifié
		/// </summary>
		/// <param name="numClient">numéro du client</param>
		/// <param name="numCompte">numéro de son compte courant</param>
		/// <exception cref="KeyNotFoundException">Client ou compte courant associé non trouvé</exception>
		public static void EnvoyerCarte(long numClient, long numCompte)
		{
			// Recherche le client dans la liste
			int indiceClient = GetIndiceClient(numClient);
			if (indiceClient < 0)
				throw new KeyNotFoundException($"Client N°{numClient} non trouvé");

			Client cli = _clients[indiceClient];

			// Recherche le compte courant et envoie la carte associée
			for (int c = 0; c < cli.Comptes.Count; c++)
			{
				if (cli.Comptes[c].Numero == numCompte && cli.Comptes[c].TypeCompte == TypesCompte.Courant)
				{
					cli.Comptes[c].CarteEnvoyée = true;
					return;
				}
			}

			// Compte courant associé non trouvé
			throw new KeyNotFoundException($"Compte courant N°{numCompte} non trouvé");
		}

		// Recherche un client dans la liste d'après son numéro et renvoie son indice ou -1 si pas trouvé
		// NB/ Plus loin dans cette formation, on utilisera un dictionnaire pour ce genre de choses
		private static int GetIndiceClient(long numClient)
		{
			int indiceLCient = -1;
			for (int c = 0; c < _clients.Count; c++)
			{
				if (_clients[c].Numéro == numClient)
				{
					indiceLCient = c;
					break;
				}
			}

			return indiceLCient;
		}
	}
}

