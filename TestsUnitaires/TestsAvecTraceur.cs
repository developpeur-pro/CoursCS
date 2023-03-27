namespace TestsUnitaires
{
	[TestClass]
	public class TestsAvecTraceur
	{
		#region Initialisation et nettoyage
		[ClassInitialize]
		public static void InitialiserTestsClasse(TestContext context)
		{
			Traceur.TracerAppel();
		}

		[ClassCleanup]
		public static void NettoyerClasse()
		{
			Traceur.TracerAppel();
		}

		[TestInitialize]
		public void InitialiserTest()
		{
			Traceur.TracerAppel();
		}

		[TestCleanup]
		public void NettoyerTest()
		{
			Traceur.TracerAppel();
		}
		#endregion

		#region Tests unitaires
		[TestMethod]
		public void TesterCreationCompte()
		{
			Traceur.TracerAppel();

			CompteBancaire cpt1 = new(123456);

			// Vérifie la valeur par défaut du libellé du compte
			Assert.AreEqual(cpt1.Libelle, $"Compte N°{cpt1.Numero}");

			// Vérifie la valeur par défaut de la date de création
			Assert.AreEqual(cpt1.DateCreation, DateOnly.FromDateTime(DateTime.Today));
		}

		[TestMethod]
		public void TesterCreditCompte()
		{
			Traceur.TracerAppel();

			CompteBancaire cpt1 = new(123456);
			Assert.AreEqual(cpt1.Solde, 0);
		}

		#endregion
	}
}
