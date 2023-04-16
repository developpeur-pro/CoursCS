namespace Héritage
{
	public class CompteTitres : CompteBancaire
	{
		public SortedList<string, Titre> Portefeuille { get; } = new();

		// Propriété redéfinie
		public override DateOnly? DateCloture
		{
			get => base.DateCloture;
			set
			{
				if (value != null && value.Value.AddDays(1).Month == value.Value.Month)
					throw new ArgumentException("Le compte ne peut être cloturé que le dernier jour du mois");

				base.DateCloture = value;
			}
		}

		#region Constructeurs
		public CompteTitres(long numero) : base(numero)
		{
		}

		public CompteTitres(long numero, DateOnly dateOuv, decimal montantInit) :
			base(numero, dateOuv, montantInit)
		{
			Libelle = "Compte titres";
		}
		#endregion

		public void AcheterTitre(string codeValeur, decimal valeurPart, decimal nbParts)
		{
			if (Solde - valeurPart * nbParts < 0)
				throw new InvalidOperationException("Liquidités insuffisantes pour cet achat.");

			if (Portefeuille.TryGetValue(codeValeur, out Titre? titre))
			{
				titre.NbParts += nbParts;
			}
			else
			{
				Portefeuille.Add(codeValeur, new Titre(codeValeur, nbParts));
				Solde -= valeurPart * nbParts;
			}
		}

		public void VendreTitre(string codeValeur, decimal valeurPart, decimal nbParts)
		{
			if (Portefeuille.TryGetValue(codeValeur, out Titre? titre))
			{
				if (nbParts > titre.NbParts)
					throw new ArgumentOutOfRangeException("", "Le nombre de parts doit être inférieur ou égal à celui détunu.");

				titre.NbParts -= nbParts;
				Solde += valeurPart * nbParts;
			}
		}

		public override void Cloturer(DateOnly date, long numCompteDest)
		{
			if (Portefeuille.Any())
				throw new InvalidOperationException("Le compte ne peut pas être cloturé tant que le portefeuille n'est pas vide.");

			base.Cloturer(date, numCompteDest);
		}

		public override string ToString()
		{
			return base.ToString() + $", {Portefeuille.Count} titres détenus";
		}

		protected override decimal CalculerFraisAnnuels()
		{
			throw new NotImplementedException();
		}
	}

	public class Titre
	{
		public string Code { get; }
		public decimal NbParts { get; set; }

		public Titre(string code, decimal nbParts)
		{
			Code = code;
			NbParts = nbParts;
		}
	}
}
