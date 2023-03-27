namespace POO
{
	public class CompteBancaire
	{
		#region Propriétés publiques
		public long Numero { get; }
		public DateOnly DateCreation { get; init; }
		public string Libelle { get; set; } = string.Empty;
		public decimal Solde { get; private set; }
		public List<Operation> Operations { get; }
		public decimal DecouvertMaxiAutorisé { get; set; } = -1000m;
		#endregion

		#region Constructeurs
		public CompteBancaire(long numero)
		{
			Numero = numero;
			DateCreation = DateOnly.FromDateTime(DateTime.Today);
			Libelle = $"Compte N°{numero}";
			Operations = new();
		}

		public CompteBancaire(long numero, DateOnly dateCreation) : this(numero)
		{
			DateCreation = dateCreation;
		}

		public CompteBancaire(long numero, DateOnly dateCreation, string libelle) :
			this(numero, dateCreation)
		{
			Libelle = libelle;
		}
		#endregion

		#region Méthodes publiques (accessibles à l’extérieur de la classe)
		public void Crediter(decimal montant)
		{
			Solde += montant;
			Operations.Add(new Operation(montant));
		}

		// Méthode non statique
		public void Debiter(decimal montant)
		{
			if (Solde - montant < DecouvertMaxiAutorisé)
				throw new ArgumentOutOfRangeException($"Débit refusé car au-delà du découvert maximum de {DecouvertMaxiAutorisé:C}");

			Solde -= montant;
			Operations.Add(new Operation(-montant));
		}

		public void Debiter(decimal montant, string libelle)
		{
			Debiter(montant);
			Operations[Operations.Count - 1].Libelle = libelle;
		}

		public void Debiter(decimal montant, string libelle, DateOnly dateEffet)
		{
			Debiter(montant, libelle);
			Operations[Operations.Count - 1].Date = dateEffet;
		}

		#endregion
	}
}