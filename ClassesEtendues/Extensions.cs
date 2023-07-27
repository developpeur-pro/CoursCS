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