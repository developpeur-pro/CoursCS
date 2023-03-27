using System.Collections.ObjectModel;
using System.Text;


namespace POO
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Unicode;

			// Instancie un compte bancaire et initialise son numéro
			CompteBancaire compte1 = new CompteBancaire(123456);
			CompteBancaire compte2 = new CompteBancaire(7556951);
			Console.WriteLine(compte1.Numero);

			// Utilisation de membres publics
			compte1.Libelle = "Compte d'épargne";
			compte1.Crediter(500);
			compte1.Debiter(123, "achat d'un truc", new DateOnly(2025, 12, 25));
			Console.WriteLine($"Le solde du compte {compte1.Libelle} est de {compte1.Solde:C2}");

			CompteBancaire cpt3 = new CompteBancaire(8563214);
			// Initialisation des propriétés en lecture / écriture sans constucteur, ni initialiseur
			//cpt3.DateCreation = new DateOnly(2030, 10, 15);
			cpt3.Libelle = "Compte courant";

			// Constructeur avec 3 paramètres
			CompteBancaire cpt4 = new CompteBancaire(8563214, new DateOnly(2030, 10, 15), "azerty");

			// Initialiseur
			CompteBancaire cpt5 = new CompteBancaire(8563214)
			{
				DateCreation = new DateOnly(2030, 10, 15),
				Libelle = "dsfsdf"
			};

			// Propriétés requises dans les initialiseurs
			Client c = new Client{ Nom = "Dupont ", Prenom = "Eric" };
		}
	}

	public class Client
	{
		public required string Nom { get; set; } = string.Empty;
		public required string Prenom { get; set; } = string.Empty;

		public string NomComplet => $"{Nom} {Prenom}";
	}
}