using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;

namespace ClassesEtendues
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//TesterMethodesExtension();

			//TesterAttributSerialisation();
			//TesterAttributsValidation();

			//AfficherInfosType();
			//ListerMembresType();
			//ListerValeursPropriétés();
			//ListerAttributsDePropriété();

			TesterGénériques();
		}

		static void TesterMethodesExtension()
		{
			string phrase = "J'aime le C#";
			int nbMots = phrase.CountWords();
			Console.WriteLine($"La phrase ({phrase}) comporte {nbMots} mots");
			//int nbMots = StringHelper.CountWords(phrase);
		}

		static void TesterAttributSerialisation()
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(List<Voiture>),
						 new XmlRootAttribute("Voitures"));

			using FileStream fs = new FileStream("Voitures.xml", FileMode.Open);
			List<Voiture>? voitures = deserializer.Deserialize(fs) as List<Voiture>;

			if (voitures != null)
			{
				foreach (Voiture v in voitures)
					Console.WriteLine($"{v.Id} : {v.Marque} {v.Modèle} de {v.Année}");
			}
		}

		static void TesterAttributsValidation()
		{
			CompteUtil compte = new();

			Console.WriteLine("Créez votre compte.");

			GérerErreur(() =>
			{
				Console.WriteLine("\nPseudo :");
				compte.Pseudo = Console.ReadLine() ?? "";
			});

			GérerErreur(() =>
			{
				Console.WriteLine("\nAdresse email :");
				compte.Email = Console.ReadLine() ?? "";
			});

			GérerErreur(() =>
			{
				Console.WriteLine("\nMot de passe :");
				compte.MotDePasse = Console.ReadLine() ?? "";
			});

			Console.WriteLine($"\nBienvenue {compte.Pseudo}, votre compte a bien été créé !");
		}

		// Gère l'affichage des erreurs de validation au cours d'une saisie
		static void GérerErreur(Action saisie)
		{
			// On répète la saisie tant qu'elle n'est pas valide
			bool erreur;
			do
			{
				erreur = false;
				try
				{
					saisie();
				}
				catch (ValidationException ve)
				{
					Console.WriteLine(ve.Message);
					erreur = true;
				}
			} while (erreur);
		}

		static void AfficherInfosType()
		{
			Console.WriteLine("Infos du type :");
			Type t1 = typeof(CompteUtil);
			Console.WriteLine($"Espace de noms : {t1.Namespace}, nom : {t1.Name}");

			Console.WriteLine("\nInfos du type à partir d'une instance :");
			CompteUtil compte = new CompteUtil("eric_dup", "edup@free.fr", "Azerty1");
			Type t2 = compte.GetType();
			Console.WriteLine($"Espace de noms : {t2.Namespace}, nom : {t2.Name}");
		}
		
		static void ListerMembresType()
		{
			// Liste des membres publics (type et nom) de la classe CompteUtil
			Type t = typeof(CompteUtil);

			Console.WriteLine($"Membres de {t.Name} :");
			foreach (MemberInfo m in t.GetMembers())
			{
				Console.WriteLine($"Type: {m.MemberType}, Nom: {m.Name}");
			}
		}

		static void ListerValeursPropriétés()
		{			
			CompteUtil compte = new CompteUtil("eric_dup", "edup@free.fr", "Azerty1");
			PropertyInfo[] props = compte.GetType().GetProperties();

			Console.WriteLine("Noms et valeurs des propriétés du compte :\n");
			foreach (PropertyInfo p in props)
			{
				Console.WriteLine($"{p.Name} = {p.GetValue(compte)}");
			}
		}

		static void ListerAttributsDePropriété()
		{
			CompteUtil compte = new CompteUtil("eric_dup", "edup@free.fr", "Azerty1");
			PropertyInfo? prop = compte.GetType().GetProperty(nameof(compte.Pseudo));

			if (prop == null) return;

			Console.WriteLine($"Attributs de la propriété {prop.Name} :");

			foreach (Attribute a in prop.GetCustomAttributes())
			{				
				if (a is StringLengthAttribute sla)
					Console.WriteLine($"{sla.GetType().Name}({sla.MinimumLength})");
				else
					Console.WriteLine($"{a.GetType().Name}");
			}
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