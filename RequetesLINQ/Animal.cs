namespace RequetesLINQ
{
	public enum Especes { Chat, Chien }
	public enum Sexes { Femelle, Male }
	public class Animal
	{
		public Animal(Especes espece, Sexes sexe, string nom)
		{
			Espece = espece;
			Sexe = sexe;
			Nom = nom;
		}

		public Animal(Especes espece, Sexes sexe, string nom, DateTime? dateNais) : this(espece, sexe, nom)
		{
			DateNais = dateNais;
		}

		public Especes Espece { get; set; }
		public Sexes Sexe { get; set; }
		public string Nom { get; set; }
		public DateTime? DateNais { get; set; }
		public int? IdRace { get; set; }

		public override string ToString()
		{
			return $"{Nom}, {Espece} {Sexe} né(e) le {DateNais:d}";
		}
	}

	public record class Race(int id, string nom);

	public record class AnimalCompagnie(Especes espece, Sexes sexe, string nom);
}
