using System.Data;


namespace TestsUnitaires
{
	[TestClass]
	public class TestCompteBancaire
	{
		private CompteBancaire _cb1;
		private static CompteBancaire _cb2;

		[ClassInitialize]
		public static void InitialiserClasse(TestContext context)
		{
			_cb2 = new CompteBancaire(222222);
		}

		// Initialisation faite avant chaque test unitaire
		[TestInitialize]
		public void InitialiserTest()
		{
			_cb1 = new CompteBancaire(823456);
			_cb1.DecouvertMaxiAutoris� = -500m;
		}
		
		[TestMethod]
		public void TesterCreationCompte()
		{
			// V�rifie la valeur par d�faut de la date de cr�ation
			Assert.AreEqual(DateOnly.FromDateTime(DateTime.Today), _cb1.DateCreation);

			// V�rifie la valeur par d�faut du libell�
			Assert.AreEqual($"Compte N�{_cb1.Numero}", _cb1.Libelle);
		}

		[DataTestMethod]
		[DataRow(20, 20, 1)]
		[DataRow(200, 220, 2)]
		[DataRow(2000, 2220, 3)]
		public void TesterCredit(double montant, double solde, int nbOp)
		{
			_cb2.Crediter((decimal)montant);

			// V�rifie le solde du compte
			Assert.AreEqual((decimal)solde, _cb2.Solde);

			// V�rifie que l'op�ration a bien �t� enregistr�e dans l'historique
			Assert.IsTrue(_cb2.Operations.Count == nbOp);
			Assert.AreEqual((decimal)montant, _cb2.Operations[nbOp - 1].Montant);
		}

		[TestMethod]
		public void TesterDebit()
		{
			decimal montant = 100m;
			string libelle = "Courses";
			_cb1.Debiter(montant, libelle);

			// V�rifie le solde du compte
			Assert.AreEqual(-montant, _cb1.Solde);

			// V�rifie que l'op�ration a bien �t� enregistr�e dans l'historique
			Assert.IsTrue(_cb1.Operations.Count == 1);
			Assert.AreEqual(-montant, _cb1.Operations[0].Montant);
			Assert.AreEqual(libelle, _cb1.Operations[0].Libelle);
			Assert.AreEqual(DateOnly.FromDateTime(DateTime.Today), _cb1.Operations[0].Date);
		}

		[TestMethod]
		public void TesterDebitAvecDateEffet()
		{
			decimal montant = 100m;
			string libelle = "Courses";
			DateOnly dateEffet = new DateOnly(2025, 12, 20);
			_cb1.Debiter(montant, libelle, dateEffet);

			// V�rifie le solde du compte
			Assert.AreEqual(-montant, _cb1.Solde);

			// V�rifie que l'op�ration a bien �t� enregistr�e dans l'historique
			Assert.IsTrue(_cb1.Operations.Count == 1);
			Assert.AreEqual(-montant, _cb1.Operations[0].Montant);
			Assert.AreEqual(libelle, _cb1.Operations[0].Libelle);
			Assert.AreEqual(dateEffet, _cb1.Operations[0].Date);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TesterDebitDepassantDecouvertMaxi()
		{
			decimal montant = 700m;
			_cb1.Debiter(montant); // doit lever une exception ArgumentOutOfRangeException
		}
	}
}