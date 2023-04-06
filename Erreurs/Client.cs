
namespace Erreurs
{
	public class Client
	{
		public long Numéro { get; set; }
		public string Nom { get; set; } = string.Empty;
		public string Prénom { get; set; } = string.Empty;
		public List<Compte> Comptes { get; } = new();
	}

	public enum TypesCompte { Courant=1, Livret, Titres }

	public class Compte
	{
		public long Numero { get; init; }
		public TypesCompte TypeCompte { get; init; }
		public bool CarteEnvoyée { get; set; }

		public Compte(long numero, TypesCompte typeCompte)
		{
			Numero = numero;
			TypeCompte = typeCompte;
		}
	}
}
