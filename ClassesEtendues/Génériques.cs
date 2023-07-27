using System.Numerics;

namespace ClassesEtendues
{
	// Interface générique pour classes identifiables
	public interface IIdentifiable<T>
	{
		public T Id { get; init; }
	}

	// Classe implémentant cette interface
	public class Article : IIdentifiable<int>, IComparable<Article>
	{
		public int Id { get; init; }
		public string Libellé { get; set; } = string.Empty;

		public int CompareTo(Article? other)
		{
			return Id.CompareTo(other?.Id);
		}
	}

	// Classe générique pour des points du plan
	public class Point<T> where T: ISignedNumber<T>
	{
		public T X { get; init; }
		public T Y { get; init; }

		public Point(T x, T y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"({X}, {Y})";
		}

		// Calcule la distance entre le point courant et le point passé en paramètre
		public double GetDistanceTo(Point<T> point)
		{
			T square = (point.X - X) * (point.X - X) + (point.Y - Y) * (point.Y - Y);
			return Math.Sqrt(Convert.ToDouble(square));
		}

		// Méthode générique pour convertir les coordonnées d'un point dans un autre type
		public Point<TDest> ConvertTo<TDest>() where TDest : ISignedNumber<TDest>
		{
			TDest x = (TDest)Convert.ChangeType(X, typeof(TDest));
			TDest y = (TDest)Convert.ChangeType(Y, typeof(TDest));
			return new Point<TDest>(x, y);
		}
	}
}
