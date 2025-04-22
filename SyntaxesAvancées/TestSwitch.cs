namespace SyntaxesAvancées;
internal static class TestSwitch
{
	public static void TestValeurs()
	{
		string[] codes = { "A4V", "A4H", "C", "X" };

		try
		{
			foreach (string code in codes)
			{
				Aspect2 a = new Aspect2(code);
				Console.WriteLine($"{code} : {a}, L/H={a.RatioLH:N3}");
			}
		}
		catch (FormatException e)
		{
			Console.WriteLine(e.Message);
		}
	}

	public static void TestTypes()
	{
		List<CompteBancaire> comptes = new List<CompteBancaire>()
		{
			new CompteCourant(10) { Solde = 100 },
			new CompteEpargne(11) { Solde = 110 },
			new CompteTitres(12) { ValeurTitres = 1200 },
			new CompteCourant(20) { Solde = 200 },
			new CompteEpargne(21) { Solde = 210 },
			new CompteTitres(22) { ValeurTitres = 2200 },
		};

		foreach (CompteBancaire compte in comptes)
		{
			decimal valeur = 0;

			if (compte is CompteEpargne ce)
				valeur = ce.Solde;
			else if (compte is CompteCourant cc)
				valeur = cc.Solde;
			else if (compte is CompteTitres ct)
				valeur = ct.ValeurTitres;
			else
				throw new NotImplementedException();

			Console.WriteLine($"Compte {compte.Numero} : {valeur:C0}");
		}
		Console.WriteLine();

		foreach (CompteBancaire compte in comptes)
		{
			decimal valeur = compte switch
			{
				CompteCourant cc => cc.Solde,
				CompteEpargne ce => ce.Solde,
				CompteTitres ct => ct.ValeurTitres,
				_ => throw new NotImplementedException()
			};

			Console.WriteLine($"Compte {compte.Numero} : {valeur:C0}");
		}
	}
}
