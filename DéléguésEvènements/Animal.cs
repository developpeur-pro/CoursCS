namespace DéléguésEvènements
{
	public class Animal
	{
		public Animal(string nom)
		{
			Nom = nom;
		}

		public string Nom { get; } = string.Empty;
		public long NumTatouage { get; set; }
		public List<ActeVétérinaire> CarnetSanté { get; } = new();

		public void EnregistrerVisiteVéto(DéléguéActesVéto délégué, DateTime date)
		{
			délégué(this, date);
		}
	}
}
