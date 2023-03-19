using System.Data.Common;
using System.Net.Http.Headers;

namespace TypesValeurRef
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TesterCopieTypeValeur();
			//TesterCopieTypeRef();
			//TesterEgalitéTypeValeur();
			//TesterEgalitéTypeRef();
			//TesterChaines();
			//TesterOperateurNull();
		}

		static void TesterCopieTypeValeur()
		{
			int n1 = 5;
			DateTime dt1 = new DateTime(); // 01/01/0001

			int n2 = n1; // copie la valeur de n1 dans n2
			DateTime dt2 = dt1; // copie la valeur de dt1 dans dt2

			ModifierValeur(n1);
			Console.WriteLine($"n1 = {n1}");	// n1 = 5
		}

		static void ModifierValeur(int n)
		{
			n = 7;
		}

		static void TesterCopieTypeRef()
		{
			Article a1 = new Article(1, "Article 1");
			Article a2 = a1; // Copie seulement la référence de a1 dans a2, mais pas l'objet

			a2.Libelle = "Article 2"; // Modifie l'unique objet en mémoire
			Console.WriteLine($"Article a1 : Id={a1.Id}, Libelle={a1.Libelle}");

			ModifierArticle(a1);
			Console.WriteLine($"Article a1 : Id={a1.Id}, Libelle={a1.Libelle}");
		}

		static void ModifierArticle(Article a)
		{
			a.Libelle = "Article modifié";
		}

		static void TesterEgalitéTypeValeur()
		{
			// Variables de types valeur
			TimeSpan ts1 = new TimeSpan(8, 50, 59);
			TimeSpan ts2 = new TimeSpan(8, 50, 59);

			string res;
			if (ts2 == ts1)
				res = "ts2 est égal à ts1";
			else
				res = "ts2 est différent de ts1";

			Console.WriteLine(res); // ts2 est égal à ts1
		}

		static void TesterEgalitéTypeRef()
		{
			// Variables de types référence
			Article a1 = new Article(1, "Article 1");
			Article a2 = new Article(1, "Article 1");

			string res;
			if (a2 == a1)
				res = "a2 est égal à a1";
			else
				res = "a2 est différent de a1";
			
			Console.WriteLine(res); // a2 est différent de a1
		}

		static void TesterChaines()
		{
			// Le test d'égalité concerne les valeurs et non les références
			string s1 = "chaîne";
			string s2 = "chaîne";
			Console.WriteLine($"{(s1 == s2 ? "s1 = s2" : "s1 != s2")}"); // s1 = s2

			// La copie se fait sur les valeurs et non les références
			string s3 = s2;
			s2 = "truc";
			Console.WriteLine($"s2 = {s2}, s3 = {s3}");  // s2 = truc, s3 = chaîne

			// Les chaînes sont immuables
			string s4 = s2.ToUpper();
			Console.WriteLine($"s2 = {s2}, s4 = {s4}");  // s2 = truc, s4 = TRUC
		}

		static void TesterOperateurNull()
		{
			Article a1 = new Article(1, "Article 1");

			string? nomFourni = string.Empty;
			if (a1.Fournisseur != null)
				nomFourni = a1.Fournisseur.Nom;

			Console.WriteLine($"Fournisseur de l'article {a1.Id} : {nomFourni}");

			// Syntaxe équivalente avec l'opérateur null :
			string? nomFourni2 = a1.Fournisseur?.Nom;
			if (nomFourni2 == null)
				nomFourni2 = string.Empty;

			// Syntaxe équivalente avec en plus l'opérateur de fusion null
			string nomFourni3 = a1.Fournisseur?.Nom ?? string.Empty;


			// Au lieu d'écrire :
			if (a1.Fournisseur == null)
				a1.Fournisseur = new Fournisseur(0, "Inconnu");

			// ... on peut écrire plus simplement :
			a1.Fournisseur ??= new Fournisseur(0, "Inconnu");
		}
	}
}