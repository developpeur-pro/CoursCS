namespace Fonctions
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int nbMots = OutilsTexte.CompterMots("j'aime le C#");

			//double[] tabNombres = { 4.21, 5.54, 2.13, 3.47 };
			double max1 = OutilsMaths.GetValeurMaxi(4.21, 5.54, 2.13, 3.47, 4.58, 69);
			Console.WriteLine("Valeur maxi : " + max1);

			string traduction = OutilsTexte.Traduire("J'aime le C#");

			(double x, double y, double z) point = (5.69, 8.47, 3.14);

			Console.WriteLine($"{point.x}, {point.y}, {point.z}");

			(double min, double max) = OutilsMaths.GetValeursMinMax(4.21, 5.54, 2.13, 3.47);

			Console.WriteLine($"Valeurs mini et maxi : {min}, {max}");

			double mini, maxi;
			OutilsMaths.GetValeursMinMax(out mini, out maxi, 4.21, 5.54, 2.13, 3.47);
			Console.WriteLine($"Valeurs mini et maxi : {mini}, {maxi}");


			// Utiliser un paramètre en out + return
			bool repOk;
			do
			{
				Console.WriteLine("Saisissez un nombre compris entre 1 et 10 :");
				string? rep = Console.ReadLine();
				repOk = int.TryParse(rep, out int nombre) && nombre >= 1 && nombre <= 10;
			} while (!repOk);
		}
	}
}