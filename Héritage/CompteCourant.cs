namespace Héritage
{
	public class CompteCourant : CompteBancaire
	{
		public decimal DecouvertMaxiAutorise { get; set; } = -1000m;

		#region Constructeurs
		public CompteCourant(long numero) : base(numero)
		{
		}

		public CompteCourant(long numero, DateOnly dateOuv, decimal montantInit) :
			base(numero, dateOuv, montantInit)
		{
			Libelle = "Compte courant";
		}
		#endregion

		public override void Debiter(decimal montant, MoyensTransaction moyen = MoyensTransaction.Carte, string libelle = "")
		{
			if (Solde - montant < DecouvertMaxiAutorise)
				throw new ArgumentOutOfRangeException("", $"Débit refusé car au-delà du découvert maximum de {DecouvertMaxiAutorise:C}");

			base.Debiter(montant, moyen, libelle);
		}

		public override string ToString()
		{
			return base.ToString() + $", découvert maxi autorisé : {DecouvertMaxiAutorise:C0}";
		}

		protected override decimal CalculerFraisAnnuels()
		{
			throw new NotImplementedException();
		}
	}
}