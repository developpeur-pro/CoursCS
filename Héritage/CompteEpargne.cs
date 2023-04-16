namespace Héritage
{
	public class CompteEpargne : CompteBancaire
	{
		public decimal TauxInterets { get; } = 0.02m;
		public decimal SoldeMin { get; } = 50m;
		public decimal SoldeMax { get; } = 100_000m;

		public CompteEpargne(long numero) : base(numero)
		{
		}

		public CompteEpargne(long numero, DateOnly dateOuv, decimal montantInit) :
			base(numero, dateOuv, montantInit)
		{
			Libelle = "Compte épargne";
		}

		public override void Crediter(decimal montant, MoyensTransaction moyen, string libelle)
		{
			if (montant + Solde > SoldeMax)
				throw new ArgumentOutOfRangeException("", $"Le livret est plafonné à {SoldeMax:C0}");

			base.Crediter(montant, moyen, libelle);
		}

		public override void Debiter(decimal montant, MoyensTransaction moyen, string libelle)
		{
			if (Solde - montant < SoldeMin)
				throw new ArgumentOutOfRangeException("", $"Il doit rester au moins {SoldeMin:C0} sur le livret.");

			base.Debiter(montant, moyen, libelle);
		}

		public override string ToString()
		{
			return base.ToString() + $", taux d'intérêts : {TauxInterets:#.00%}";
		}

		public void CrediterInterets()
		{
			Solde *= (1+ TauxInterets / 12m);
		}

		protected override decimal CalculerFraisAnnuels()
		{
			throw new NotImplementedException();
		}
	}
}
