using System.ComponentModel.DataAnnotations;

namespace ClassesEtendues
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//TesterMethodesExtension();
			//TesterGénériques();
		}

		static void TesterMethodesExtension()
		{
			string phrase = "J'aime le C#";
			int nbMots = phrase.CountWords();
			Console.WriteLine($"La phrase ({phrase}) comporte {nbMots} mots");
			StringHelper.CountWords(phrase);

			int x = 3, y = 7;
			string res = x.CompareToInString(y);

			Console.WriteLine($"x {res} y");
			Console.WriteLine($"x {x.CompareToInString(y)} y");
		}

		static void TesterGénériques()
		{
			//Article a1 = new Article { Id = 1, Libellé = "Tasse" };
			//Article a2 = new Article { Id = 2, Libellé = "Mug" };

			Point<int> A = new Point<int>(3, 4);
			Point<int> B = new Point<int>(5, 6);
			double d1 = A.GetDistanceTo(B);
			Console.WriteLine($"Distance de {A} à {B} : {d1}");

			Point<float> C = new Point<float>(3.3f, 4.4f);
			Point<float> D = new Point<float>(5.5f, 6.6f);
			double d2 = C.GetDistanceTo(D);
			Console.WriteLine($"Distance de {C} à {D} : {d2}");

			Point<int> E = D.ConvertTo<int>();
			Console.WriteLine($"{D} converti en Point<int> : {E}");
		}
	}
}