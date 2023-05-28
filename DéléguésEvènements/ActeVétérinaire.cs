namespace DéléguésEvènements
{
	public delegate void DéléguéActesVéto(Animal animal, DateTime date);

	public class ActeVétérinaire
	{
		public ActeVétérinaire(DateTime date, string description)
		{
			Date = date;
			Description = description;
		}

		public DateTime Date { get; set; }
		public string Description { get; set; } = string.Empty;

		#region Méthodes statiques
		public static void Tatouer(Animal a, DateTime date)
		{
			a.NumTatouage = date.Year*10000 + date.Month*100 + date.Day;
			ActeVétérinaire acte = new (date, $"Tatouage avec le numéro {a.NumTatouage}");
			a.CarnetSanté.Add(acte);
		}

		public static void Déparasiter(Animal a, DateTime date)
		{
			ActeVétérinaire acte = new (date, $"Dépérasitage. A renouveler le {date.AddMonths(2):d}");
			a.CarnetSanté.Add(acte);
		}

		public static void Peser(Animal a, DateTime date)
		{
			ActeVétérinaire acte = new(date, $"Pesée");
			a.CarnetSanté.Add(acte);
		}
		#endregion
	}
}
