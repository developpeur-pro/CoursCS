namespace RequetesLINQ
{
	internal static class TestLinq
	{
		private static List<Animal> _anx;

		static TestLinq()
		{
			DateTime dt = DateTime.Today;

			_anx = new List<Animal>()
			{
				new Animal(Especes.Chat, Sexes.Femelle, "Blanchette", dt.AddDays(-400)),
				new Animal(Especes.Chat, Sexes.Femelle, "Zapette", dt.AddDays(-1000)),
				new Animal(Especes.Chat, Sexes.Femelle, "Choupette", dt.AddDays(-200)),
				new Animal(Especes.Chat, Sexes.Male, "Ponpon", dt.AddDays(-1400)),
				new Animal(Especes.Chat, Sexes.Male, "Grison", dt.AddDays(-900)),
				new Animal(Especes.Chat, Sexes.Male, "Riton", dt.AddDays(-2000)),
				new Animal(Especes.Chien, Sexes.Femelle, "Rita",dt.AddDays(-800)),
				new Animal(Especes.Chien, Sexes.Femelle, "Zita", null),
				new Animal(Especes.Chien, Sexes.Femelle, "Zora", dt.AddDays(-300)),
				new Animal(Especes.Chien, Sexes.Male, "Woolfy", dt.AddDays(-1600)),
				new Animal(Especes.Chien, Sexes.Male, "Rooky", dt.AddDays(-300)),
				new Animal(Especes.Chien, Sexes.Male, "Skipy", dt.AddDays(-600))
			};
		}

		public static void Filtrer()
		{
			// Extraction des nombres pairs d'un tableau
			int[] entiers = new int[] { 1, 2, 3, 4, 5, 6 };
			IEnumerable<int> pairs = from n in entiers
											 where n % 2 == 0
											 select n;

			var pairs2 = entiers.Where(n => n % 2 == 0);

			foreach (int p in pairs) Console.Write($"{p},"); // 2,4,6

			// Liste des noms des chats mâles
			IEnumerable<string> nomsChats =
				from a in _anx
				where a.Espece == Especes.Chat && a.Sexe == Sexes.Male
				select a.Nom;

			// Syntaxe pointée
			IEnumerable<string> nomsChats2 = 
				_anx.Where(a => a.Espece == Especes.Chat && a.Sexe == Sexes.Femelle)
				.Select(a => a.Nom);

			Console.Write("\nNoms de chats mâles : ");
			foreach (string nom in nomsChats) Console.Write(nom + ", ");
		}

		public static void Trier()
		{
			// Chiens du plus vieux au plus jeune
			IOrderedEnumerable<Animal> anis =
				from a in _anx
				where a.Espece == Especes.Chien
				orderby a.DateNais, a.Nom ascending
				select a;

			var anis2 = _anx.Where(a => a.Espece == Especes.Chien)
				.OrderBy(a => a.DateNais).OrderBy(a => a.Nom);

			Console.WriteLine("\nChiens triés par âge croissant :");
			foreach (Animal a in anis) Console.WriteLine(a);

			// Animal le plus vieux
			Animal? ani = (from a in _anx
								where a.DateNais != null
								orderby a.DateNais
								select a).FirstOrDefault();

			Animal? ani2 = _anx.Where(a=> a.DateNais != null).OrderBy(a => a.DateNais).FirstOrDefault();

			Console.WriteLine("\nAnimal le plus vieux : " + ani);
		}

		public static void MinMax()
		{
			// Date de naissance la plus ancienne
			DateTime? dtMin = (from a in _anx
									 select a.DateNais).Min();

			DateTime? dtMin2 = _anx.Min(a => a.DateNais);

			// Animal le plus vieux
			Animal? ani = (from a in _anx
								where a.DateNais == dtMin
								select a).FirstOrDefault();

			Animal? ani2 = _anx.Where(a => a.DateNais == dtMin2).FirstOrDefault();

			Console.WriteLine("\nAnimal le plus vieux : " + ani);
		}

		public static void CalculerAgrégats()
		{
			// Nombre d'animaux femelles
			int nbFemelles = (from a in _anx 
									where a.Sexe == Sexes.Femelle
									select 1).Count();

			int nbFemelles2 = _anx.Count(a => a.Sexe == Sexes.Femelle);

			Console.WriteLine($"{nbFemelles} animaux femelles");

			// Age moyen des animaux
			double ageMoyen = (from a in _anx
									 where a.DateNais != null
									 select (DateTime.Today - a.DateNais!).Value.Days).Average();

			double ageMoyen2 = _anx.Where(a => a.DateNais != null)
				.Average(a => (DateTime.Today - a.DateNais!).Value.Days);

			Console.WriteLine($"Age moyen des animaux : {ageMoyen:N1} jours");
		}

		public static void Regrouper()
		{
			// Regroupe les animaux par année de naissance
			IEnumerable<IGrouping<int, Animal>> groupes =
				from a in _anx
				where a.DateNais != null
				orderby a.DateNais!.Value.Year
				group a by a.DateNais!.Value.Year;

			IEnumerable<IGrouping<int, Animal>> groupes2 =
				_anx.Where(a => a.DateNais != null)
				.OrderBy(a => a.DateNais)
				.GroupBy(a => a.DateNais!.Value.Year);

			// Affiche les animaux de chaque groupe
			foreach (var gp in groupes)
			{
				string txt = gp.Count() > 1 ? "Animaux nés" : "Animal né";
				Console.Write($"\n{txt} en {gp.Key} : ");
				foreach (var ani in gp)
				{
					Console.Write($"{ani.Nom} ");
				}
			}
		}

		public static void Transformer()
		{
			// Regroupe les animaux par année de naissance
			IEnumerable<IGrouping<int, Animal>> groupes =
				from a in _anx
				where a.DateNais != null
				orderby a.DateNais!.Value.Year
				group a by a.DateNais!.Value.Year;

			// Transforme le résultat en liste de tuples (année, nb animaux)
			List<(int, int)> liste = (from g in groupes
					select (g.Key, g.Count())).ToList();

			List<(int, int)> liste2 = groupes.Select(gp => (gp.Key, gp.Count())).ToList();

			Console.WriteLine("\nListe de tuples :");
			foreach ((int année, int nbNais) in liste)
			{
				Console.WriteLine($"Animaux nés en {année} : {nbNais}");
			}

			// Transforme le résultat en dictionnaire dont la clé est l'année
			// et la valeur est le nombre de chiens nés cette année là
			var dico = groupes.ToDictionary(g => g.Key, g => g.Count(a => a.Espece == Especes.Chien));

			Console.WriteLine("\nDictionnaire :");
			foreach (var kvp in dico)
			{
				Console.WriteLine($"Chiens nés en {kvp.Key} : {kvp.Value}");
			}
		}
	}
}