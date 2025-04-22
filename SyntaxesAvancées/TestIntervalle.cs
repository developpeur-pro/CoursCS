namespace SyntaxesAvancées;
internal static class TestIntervalle
{
	public static void TestRange()
	{
		string[] tableau = ["un", "deux", "trois", "quatre", "cinq"];

		Afficher(tableau[new Range(1, 3)]);    // deux trois

		Afficher(tableau[Range.StartAt(2)]);   // trois quatre cinq

		Afficher(tableau[Range.EndAt(2)]);     // un deux
	}

	public static void TestOperateurSurTableau()
	{
		string[] tableau = ["un", "deux", "trois", "quatre", "cinq"];
		Afficher(tableau[1..3]);   // deux trois
		Afficher(tableau[2..]);    // trois quatre cinq
		Afficher(tableau[..2]);    // un deux

		Console.WriteLine(tableau[1..3].SequenceEqual(["deux", "trois"]));
	}

	public static void TestOperateurSurListe()
	{
		List<string> liste = ["un", "deux", "trois", "quatre", "cinq"];
		List<string> liste13 = liste[1..3];    // deux trois
		List<string> liste2Fin = liste[2..];   // trois quatre cinq
		List<string> listeDeb2 = liste[..2];   // un deux
	}

	public static void TestAssemblageTableaux()
	{
		char[] voyelles = ['a', 'e', 'i', 'o', 'u'];
		char[] consonnes = ['b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm',
					'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z'];
		char[] alphabet = [..voyelles, ..consonnes, 'y'];

		Afficher(alphabet);
	}

	public static void TestExtractionChaine()
	{
		string s = "un petit essai";

		// Vérifie si la chaîne commence par "un"
		Console.WriteLine(s.StartsWith("un"));
		Console.WriteLine(s[..2] == "un");

		// Vérifie si la chaîne finit par "essai"
		Console.WriteLine(s.EndsWith("essai"));
		Console.WriteLine(s[9..] == "essai");
		Console.WriteLine(s[(s.Length - 5)..] == "essai");

		// Extrait le mot "petit"
		Console.WriteLine(s.Substring(3, 5));
		Console.WriteLine(s[3..9]);
		Console.WriteLine(s[3..(s.Length - 6)]);
	}

	public static void TestOperateurIndiceFin()
	{
		string s = "un petit essai";

		// Extrait les 5 derniers caractères de la chaîne
		Console.WriteLine(s[^5..]); // essai

		// Extrait les caractères d'indices 3 à s.Length-6
		Console.WriteLine(s[3..^6]); // petit
	}

	public static void Afficher<T>(IEnumerable<T> liste)
	{
		foreach (T s in liste) Console.Write(s + " ");

		Console.WriteLine();
	}
}
