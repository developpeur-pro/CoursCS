using System.Diagnostics;

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

		public static void SélectionnerFiltrer()
		{
			// Sélection d'une propriété
			IEnumerable<string> noms = from a in _anx select a.Nom;
			IEnumerable<string> noms2 = _anx.Select(a => a.Nom);

			// Sélection de plusieurs propriétés sous forme de tuple
			IEnumerable<(string, Especes)> anis = from a in _anx select (a.Nom, a.Espece);
			IEnumerable<(string, Especes)> anis2 = _anx.Select(a => (a.Nom, a.Espece));

			// Sélection sous forme d'un autre type (ici un record)
			IEnumerable<AnimalCompagnie> anis3 =
				from a in _anx
				select new AnimalCompagnie(a.Espece, a.Sexe, a.Nom);

			// Sélection d'un élément unique (ici, l'animal nommé Woolfy)
			Animal? ani = (from a in _anx
								where a.Nom == "Woolfy"
								select a).FirstOrDefault();

			Animal? ani2 = _anx.Where(a => a.Nom == "Woolfy").FirstOrDefault();
			Animal? ani3 = _anx.FirstOrDefault(a => a.Nom == "Woolfy");
			Animal? ani4 = _anx.SingleOrDefault(a => a.Nom == "Woolfy"); // Lève une erreur si plusieurs elts

			// Liste des noms des chats mâles
			IEnumerable<string> nomsChats =
				from a in _anx
				where a.Espece == Especes.Chat && a.Sexe == Sexes.Male
				select a.Nom;

			IEnumerable<string> nomsChats2 =
				_anx.Where(a => a.Espece == Especes.Chat && a.Sexe == Sexes.Femelle)
				.Select(a => a.Nom);

			Console.Write("\nNoms des chats mâles : ");
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
				.OrderBy(a => a.DateNais).ThenBy(a => a.Nom);

			Console.WriteLine("\nChiens triés par âges et noms croissants :");
			foreach (Animal a in anis) Console.WriteLine(a);

			Stopwatch sw = new();
			sw.Start();
			// Animal le plus vieux
			Animal? ani = (from a in _anx
								where a.DateNais != null
								orderby a.DateNais
								select a).FirstOrDefault();
			sw.Stop();
			Console.WriteLine($"\nTemps exécution requête avec tri : {ani.Nom} {sw.ElapsedTicks}");

			sw.Reset();
			sw.Start();
			Animal? x = (from a in _anx
							 where a.DateNais == _anx.Min(b => b.DateNais)
							 select a).FirstOrDefault();
			sw.Stop();
			Console.WriteLine($"\nTemps exécution requête avec min : {x.Nom} {sw.ElapsedTicks}");

			Animal? ani2 = _anx.Where(a => a.DateNais != null).OrderBy(a => a.DateNais).FirstOrDefault();

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
			// Trie et regroupe les animaux par année de naissance croissante
			IEnumerable<IGrouping<int, Animal>> groupes =
				from a in _anx
				where a.DateNais != null
				orderby a.DateNais!.Value.Year
				group a by a.DateNais!.Value.Year;

			IEnumerable<IGrouping<int, Animal>> groupes2 =
				_anx.Where(a => a.DateNais != null)
				.OrderBy(a => a.DateNais)
				.GroupBy(a => a.DateNais!.Value.Year);

			// Regroupe les animaux par année de naissance et trie les groupes ensuite
			IEnumerable<IGrouping<int, Animal>> groupes3 =
				from a in _anx
				where a.DateNais != null
				group a by a.DateNais!.Value.Year into groupeAnnée
				orderby groupeAnnée.Key
				select groupeAnnée;

			IEnumerable<IGrouping<int, Animal>> groupes4 =
				_anx.Where(a => a.DateNais != null)
				.GroupBy(a => a.DateNais!.Value.Year)
				.OrderBy(g => g.Key);

			// Affiche les animaux de chaque groupe
			foreach (var gp in groupes3)
			{
				string txt = gp.Count() > 1 ? "Animaux nés" : "Animal né";
				Console.Write($"\n{txt} en {gp.Key} : ");
				foreach (var ani in gp)
				{
					Console.Write($"{ani.Nom} ");
				}
			}

			// Calcule les nombres de chats et de chiens dans chaque groupe
			foreach (var grp in groupes3)
			{
				int nbChats = (from a in grp
									where a.Espece == Especes.Chat
									select 1).Count();

				int nbChiens = (from a in grp
									 where a.Espece == Especes.Chien
									 select 1).Count();

				Console.WriteLine($"Nés en {grp.Key} : {nbChats} chat(s) et {nbChiens} chien(s)");
			}
		}

		public static void SousRequetes()
		{
			// Animaux dont l'âge est > à l'âge moyen des animaux
			Stopwatch sw = new();
			sw.Start();
			double ageMoy = (from a in _anx
								  where a.DateNais != null
								  select (DateTime.Today - a.DateNais)!.Value.Days).Average();

			IEnumerable<Animal> ainés =
				from a in _anx
				where a.DateNais != null && (DateTime.Today - a.DateNais).Value.Days > ageMoy
				select a;

			ainés.ToList();
			sw.Stop();
			Console.WriteLine($"nTemps d'exécution : {sw.ElapsedTicks}");
			foreach (Animal a in ainés) Console.WriteLine(a);

			// Même résultat en 1 requête avec sous-requête :
			sw.Reset();
			sw.Start();
			IEnumerable<Animal> ainés2 =
				from a in _anx
				where a.DateNais != null && (DateTime.Today - a.DateNais).Value.Days >
					(from b in _anx
					 where b.DateNais != null
					 select (DateTime.Today - b.DateNais)!.Value.Days).Average()
				select a;

			ainés2.ToList();
			sw.Stop();
			Console.WriteLine($"\nTemps d'exécution 1 requête : {sw.ElapsedTicks}");
			foreach (Animal a in ainés2) Console.WriteLine(a);

			// Regroupe les animaux par année de naissance et compte les 
			// nombres de chats et de chiens dans chaque groupe avec des sous-requêtes
			IEnumerable<(int, int, int)> stats =
				from a in _anx
				where a.DateNais != null
				orderby a.DateNais!.Value.Year
				group a by a.DateNais!.Value.Year into groupes
				select (groupes.Key,
					groupes.Count(a => a.Espece == Especes.Chat),
					groupes.Count(a => a.Espece == Especes.Chien));

			Console.WriteLine("\nListe de tuples :");
			foreach ((int année, int nbChats, int nbChiens) in stats)
			{
				Console.WriteLine($"Nés en {année} : {nbChats} chat(s) et {nbChiens} chien(s)");
			}

			// Même chose en stockant le résultat dans un dictionnaire dont la clé est l'année
			// et la valeur est un tuple des nombres de chiens et de chiens nés cette année là
			Dictionary<int, (int, int)> dico =
				(from a in _anx
				 where a.DateNais != null
				 orderby a.DateNais!.Value.Year
				 group a by a.DateNais!.Value.Year)
				.ToDictionary(g => g.Key, g =>
					(g.Count(a => a.Espece == Especes.Chat),
					g.Count(a => a.Espece == Especes.Chien)));

			Console.WriteLine("\nDictionnaire :");
			foreach (var kvp in dico)
			{
				Console.WriteLine($"Nés en {kvp.Key} : {kvp.Value.Item1} chat(s) et {kvp.Value.Item2} chien(s)");
			}
		}

		public static void OperationsEnsembles()
		{
			// Autre ensemble d'animaux
			DateTime dt = DateTime.Today;
			var anx2 = new List<Animal>()
			{
				new Animal(Especes.Chat, Sexes.Femelle, "Grisette", dt.AddDays(-500)),
				new Animal(Especes.Chat, Sexes.Male, "Platon", dt.AddDays(-9000)),
				new Animal(Especes.Chien, Sexes.Femelle, "Trinita", dt.AddDays(-1700)),
				new Animal(Especes.Chat, Sexes.Male, "Citron", dt.AddDays(-1200)),
			};

			// Réunion de 2 ensembles d'animaux
			IEnumerable<Animal> chats =
				(from a in _anx
				 where a.Espece == Especes.Chat
				 select a).Concat(
				from a in anx2
				where a.Espece == Especes.Chat
				select a);

			IEnumerable<Animal> chats2 = _anx.Where(a => a.Espece == Especes.Chat)
				.Concat(anx2.Where(a => a.Espece == Especes.Chat));

			Console.Write("\nEnsemble des chats :");
			foreach (Animal a in chats) Console.Write(a.Nom + ", ");
			Console.WriteLine();

			// Intersection
			var chiensMales =
				(from a in _anx
				 where a.Sexe == Sexes.Male
				 select a.Nom)
				.Intersect(
				from a in _anx
				where a.Espece == Especes.Chien
				select a.Nom);

			Console.WriteLine("\nChiens mâles :");
			foreach (string nom in chiensMales) Console.WriteLine(nom);

			// Exclusion
			var chatsMales =
				(from a in _anx
				 where a.Sexe == Sexes.Male
				 select a.Nom)
				.Except(
				from a in _anx
				where a.Espece == Especes.Chien
				select a.Nom);

			Console.WriteLine("\nChats mâles :");
			foreach (string nom in chatsMales) Console.WriteLine(nom);
	
		}

		public static void Jointure()
		{
			List<Animal> anx2 = new() {
				new Animal(Especes.Chat, Sexes.Femelle, "Grisette") { IdRace = 3 },
				new Animal(Especes.Chat, Sexes.Male, "Platon") { IdRace = 5 },
				new Animal(Especes.Chien, Sexes.Femelle, "Trinita") { IdRace = 7 },
				new Animal(Especes.Chat, Sexes.Male, "Citron") { IdRace = 1 }
			};

			List<Race> races = new()
			{
				new Race(1, "Maine Cool"),
				new Race(2, "Bengal"),
				new Race(3, "Chartreux"),
				new Race(4, "British Shortair"),
				new Race(5, "Norvégien"),
				new Race(6, "Staffordshire Bull Terrier"),
				new Race(7, "Golden Retriever"),
				new Race(8, "Berger Allemand"),
				new Race(9, "Labrador Retriever"),
				new Race(10,"Beagle")
			};

			var req = from a in anx2
						 join r in races on a.IdRace equals r.id
						 select (a.Espece, a.Nom, r.nom);

			var req2 = anx2.Join(races, a => a.IdRace, r => r.id, (a, r) => (a.Espece, a.Nom, r.nom));

			foreach ((Especes espèce, string nomAni, string nomRace) in req2)
			{
				Console.WriteLine($"{espèce} {nomAni} de race {nomRace}");
			}
		}
	}
}