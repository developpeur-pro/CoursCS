namespace CollectionsGeneriques
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

		public Especes Espece { get; set; }
		public Sexes Sexe { get; set; }
		public string Nom { get; set; }
	}

	public class Client : IComparable<Client>
	{
		public Client(string id, string nom, string prénom)
		{
			Id = id;
			Nom = nom;
			Prénom = prénom;
		}

		public string Id { get; }
		public string Nom { get; }
		public string Prénom { get; set; }

		public override string ToString()
		{
			return $"{Id} - {Nom} {Prénom}";
		}

		public int CompareTo(Client? other)
		{
			return (Nom + Prénom).CompareTo(other?.Nom + other?.Prénom);
		}
	}

	public class Ville
	{
		public Ville(string nom)
		{
			Nom = nom;
		}

		public string Nom { get; set; }
	}

	public class Etape
	{
		public Etape(int num, string texte, int durée = 0)
		{
			Num = num;
			Texte = texte;
			Durée = durée;
		}

		public int Num { get; }
		public string Texte { get; } = string.Empty;
		public int Durée { get; }

		public override string ToString()
		{
			return $"{Num}. {Texte}";
		}
	}

	public record class Produit(int Id, string Libellé);

	// Code généré automatiquement par le compilateur
	//public record class Produit
	//{
	//	public int Id { get; init; }
	//	public string Libellé { get; init; }

	//	public Produit(int id, string libellé)
	//	{
	//		Id = id;
	//		Libellé = libellé;
	//	}

	//	public void Deconstruct(out int id, out string libellé)
	//	{
	//		id = Id;
	//		libellé = Libellé;
	//	}
	//}
	
	public record Vêtement(int Id, string Libellé, string Taille) : Produit(Id, Libellé);

	public readonly struct Point
	{
		public Point(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public double X { get; }
		public double Y { get; }
		public double Z { get; }
	}
}