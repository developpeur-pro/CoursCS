namespace Héritage
{
	public enum MoyensTransaction { Carte, Virement }

	public abstract class CompteBancaire
	{
		#region Propriétés publiques
		public long Numero { get; }
		public DateOnly DateOuverture { get; init; }
		public virtual DateOnly? DateCloture { get; set; }
		public string Libelle { get; set; } = string.Empty;
		public decimal Solde { get; protected set; }
		public List<Operation> Operations { get; } = new();
		#endregion

		#region Constructeurs
		public CompteBancaire(long numero)
		{
			Numero = numero;
			DateOuverture = DateOnly.FromDateTime(DateTime.Today);
		}

		public CompteBancaire(long numero, DateOnly dateOuv, decimal montantInit) : this(numero)
		{
			DateOuverture = dateOuv;
			Solde = montantInit;
		}
		#endregion

		#region Méthodes publiques
		public virtual void Crediter(decimal montant, MoyensTransaction moyen, string libelle)
		{
			Solde += montant;

			Operation op = new(montant, moyen);
			if (libelle != string.Empty) op.Libelle = libelle;
			Operations.Add(op);
		}

		public virtual void Debiter(decimal montant, MoyensTransaction moyen, string libelle)
		{
			Solde -= montant;

			Operation op = new(-montant, moyen);
			if (!string.IsNullOrWhiteSpace(libelle)) op.Libelle = libelle;
			Operations.Add(op);
		}

		public virtual void Cloturer(DateOnly date, long numCompteDest)
		{
			if (numCompteDest <= 0)
				throw new InvalidOperationException("Le N° du compte pour le virement du solde doit être > 0");

			DateCloture = date;
		}

		public override string ToString()
		{
			return $"{Libelle} N°{Numero} ouvert le {DateOuverture:d}";
		}
		#endregion

		protected abstract decimal CalculerFraisAnnuels();
	}

	public class Operation
	{
		public DateOnly Date { get; }
		public decimal Montant { get; }
		public MoyensTransaction Moyen { get; }
		public string Libelle { get; set; }

		public Operation(decimal montant, MoyensTransaction moyen)
		{
			Date = DateOnly.FromDateTime(DateTime.Today);
			Montant = montant;
			Moyen = moyen;
			Libelle = (montant > 0 ? "Crédit" : "Débit") + " par " + Moyen;
		}
	}
}