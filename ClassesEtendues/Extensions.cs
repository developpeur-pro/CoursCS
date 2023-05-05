using System.Reflection.Metadata.Ecma335;

namespace ClassesEtendues
{
	// Extensions de la classe String
	public static class StringHelper
	{
		public static int CountWords(this string s)
		{
			string[] tabMots = s.Trim().Split(' ', '\'', '\t', '\n');
			return tabMots.Length;
		}

	}

	// Extensions de l'interface IComparable
	public static class IComparableExtension
	{
		public static string CompareToInString(this IComparable comp, object obj)
		{
			int n = comp.CompareTo(obj);

			if (n < 0) return "<";
			if (n > 0) return ">";
			return "=";
		}

		public static string CompareToInString<T>(this IComparable<T> comp, T other)
		{
			int res = comp.CompareTo(other);
			return res switch
			{
				-1 => "<",
				0 => "=",
				_ => ">"
			};
		}
	}

	// Extension d'une classe DTO
	public class Personne
	{
		public int Id { get; init; }
		public string Nom { get; init; } = string.Empty;
		public string Prénom { get; init; } = string.Empty;
	}

	public static class PersonneExtensions
	{
		public static string NomComplet(this Personne p)
		{
			return $"{p.Prénom} {p.Nom}";
		}
	}
}