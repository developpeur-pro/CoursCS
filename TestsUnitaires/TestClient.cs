namespace TestsUnitaires
{
	[TestClass]
	public class TestClient
	{
		[AssemblyInitialize]
		public static void InitialiserAssembly(TestContext context)
		{
			
			Traceur.TracerAppel();
		}

		[AssemblyCleanup]
		public static void NettoyerAssembly()
		{
			Traceur.TracerAppel();
		}

		[ClassInitialize]
		public static void InitialiserClasse(TestContext context)
		{
			Traceur.TracerAppel();
		}

		[ClassCleanup]
		public static void NettoyerClasse()
		{
			Traceur.TracerAppel();
		}

		[TestMethod]
		public void TesterCreationClient()
		{
			Traceur.TracerAppel();
			Client c = new Client { Nom = "Dupont", Prenom = "" };
			Assert.AreEqual(c.NomComplet, c.Nom + " " + c.Prenom);
		}
	}
}
