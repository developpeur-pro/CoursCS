using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SyntaxesAvancées;
internal static class TestRegex
{
	public static void TestIsMatch()
	{
		// Numéros à tester
		string[] numéros = ["+33123456789", "1234567890", "123456", "1234567890123456", "++33123456789"];
		//string[] nnuméros = ["0612345678", "0712345678", "06123456", "0123456789"];

		// Modèle représentant un numéro sur 10 à 15 chiffres précédés d'un + facultatif
		string modèle = @"^\+?\d{10,15}$";
		//string modèle = @"^0[6|7]\d{8}$";

		foreach (string num in numéros)
		{
			string res = Regex.IsMatch(num, modèle) ? "OK" : "incorrect";

			Console.WriteLine($"{num} : {res}");
		}
	}

	public static void TestMatch()
	{
		// Charge le texte du fichier html
		string txt = File.ReadAllText("doc.html");

		string modèle = @"<title>(.*)</title>";

		// Récupère le titre du document
		Match m = Regex.Match(txt, modèle, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		if (m.Success)
		{
			Console.WriteLine(m.Groups[1].Value.Trim());
		}
	}

	public static void TestMatches()
	{
		// Charge le texte du fichier html
		string txt = File.ReadAllText("doc.html");

		string modèle = @"<h\d>(\d+\.?\d*)\.\s(.*)</h\d>";

		Dictionary<string, string> titres = new();

		// Récupère les titres h1, h2, h3...
		foreach (Match match in Regex.Matches(txt, modèle, RegexOptions.IgnoreCase))
		{
			titres.TryAdd(match.Groups[1].Value, match.Groups[2].Value);
		}

		// Affiche le contenu du dico
		foreach (var item in titres)
		{
			Console.WriteLine($"{item.Key} {item.Value}");
		}
	}

	public static void TestReplace()
	{
		// Charge le texte du fichier html
		string txt = File.ReadAllText("doc.html");

		string modèleTitres = @"(<h\d>)(\d+)(\.?)";

		// Transforme les éléments correspondant au modèle à l'aide d'une méthode anonyme
		// et renvoie le nouveau texte complet
		string nouveauTexte = Regex.Replace(txt, modèleTitres, match =>
		{
			if (!short.TryParse(match.Groups[2].Value, out short num) || num > 26) return "";

			string res = $"{match.Groups[1].Value}{(char)(num + 64)}{match.Groups[3].Value}";
			return res;
		});

		Console.WriteLine(nouveauTexte);
	}

	public static void TestReplace2()
	{
		// Charge le texte du fichier html
		string txt = File.ReadAllText("doc.html");

		string modèleTitres = @"<h(?<niveau>\d)>\d+\.?\d*\.\s";

		// Supprime les numéros de titres et renvoie le nouveau texte complet
		string nouveauTexte = Regex.Replace(txt, modèleTitres, "<h${niveau}>");

		Console.WriteLine(nouveauTexte);
	}
}
