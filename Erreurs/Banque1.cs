
namespace Erreurs
{
	public enum RésultatsOuvertureCompte { OK, NumClientInvalide, TypeCompteDéjàExistant }

	public static class Banque1
	{
		private static List<Client> _clients = new();

		// Enregistre un nouveau client et renvoie son numéro en paramètre de sortie
		// Renvoie false si le nom ou le prénom du client est incorrect, true sinon 
		public static bool EnregistrerClient(Client personne, out long numClient)
		{
			numClient = 0;

			// Vérifie les infos du client potentiel
			if (string.IsNullOrWhiteSpace(personne.Nom) || string.IsNullOrWhiteSpace(personne.Prénom))
				return false;

			// Attribue un N° au client
			numClient = long.Parse(DateTime.Now.ToString("yyMMddHHmmss"));

			// L'enregistre dans la liste des clients
			Client client = new()
			{
				Numéro = numClient,
				Nom = personne.Nom.Trim(),
				Prénom = personne.Prénom.Trim(),
			};
			_clients.Add(client);

			return true;
		}

		// Ouvre un compte du type spécifié pour le client spécifié
		// et renvoie son numéro en paramètre de sortie
		// Renvoie NumClientInvalide si le numéro de client n'est pas valide,
		// TypeCompteDéjàExistant si un compte de même type existe déjà, ou OK si pas de problème
		public static RésultatsOuvertureCompte OuvrirCompte(long numClient, TypesCompte type, out long numCompte)
		{
			numCompte = 0;

			// Recherche le client dans la liste
			int indiceLCient = GetIndiceClient(numClient);
			if (indiceLCient == -1)
				return RésultatsOuvertureCompte.NumClientInvalide;

			// Vérifie si un compte de même type existe déjà
			if (_clients[indiceLCient].Comptes.Exists(c => c.TypeCompte == type))
				return RésultatsOuvertureCompte.TypeCompteDéjàExistant;

			// Ouvre le compte
			numCompte = long.Parse(numClient.ToString() + (int)type);
			_clients[indiceLCient].Comptes.Add(new Compte(numCompte, type));

			return RésultatsOuvertureCompte.OK;
		}

		// Envoie la carte associée au compte courant du client spécifié
		// Renvoie false si le client ou le compte n'existent pas
		// ou si le compte n'est pas un compte courant
		public static bool EnvoyerCarte(long numClient, long numCompte)
		{
			// Recherche le client dans la liste
			int indiceClient = GetIndiceClient(numClient);
			if (indiceClient < 0) return false;
			Client cli = _clients[indiceClient];	

			// Recherche le compte courant et envoie la carte associée
			for (int c = 0; c < cli.Comptes.Count; c++)
			{
				if (cli.Comptes[c].Numero == numCompte && cli.Comptes[c].TypeCompte == TypesCompte.Courant)
				{
					cli.Comptes[c].CarteEnvoyée = true;
					return true;
				}
			}

			// Compte courant spécifié non trouvé
			return false;
		}

		// Recherche un client dans la liste d'après son numéro et renvoie son indice
		// ou -1 si pas trouvé
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
