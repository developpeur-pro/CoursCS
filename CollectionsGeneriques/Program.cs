
namespace CollectionsGeneriques
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TesterListe();
			//TesterDico_MéthodesCommunes();
			//TesterDico_MethodesSpécifiques();
			//TesterListeChaînée();
			//TesterFile();
			//TesterPile();
			//TesterEnsembles();
			//TesterRecords();
		}

		static void TesterListe()
		{		
			// Initialise le contenu de la liste avec un initialiseur
			List<Client> clients = new()
			{
				new Client("EDUP", "Dupont", "Eric"),
				new Client("LLEN", "Lenoir", "Lucie"),
				new Client("PBEL", "Belin", "Pauline"),
				new Client("ALEN", "Lenoir", "Alain")
			};

			// Initialise une liste à partir d'une autre
			List<Client> clients2 = new(clients);

			#region Parcourir la liste
			// Parcourt la liste à l'envers et supprime les clients dont le prénom commence par L
			for (int i = clients.Count - 1; i >= 0; i--)
			{
				if (clients[i].Prénom.StartsWith("L"))
					clients.RemoveAt(i);
			}

			// Mais pour supprimer des éléments selon un critère,
			// il est plus simple de faire ceci :
			clients.RemoveAll(c => c.Prénom.StartsWith("L"));

			// Exécute une action sur chaque élément 
			clients.ForEach(c => Console.WriteLine(c));
			#endregion

			#region Trier et inverser l'ordre des éléments
			// Tri selon l'ordre défini par Client.CompareTo
			Console.WriteLine("\nTri");
			clients.Sort();
			foreach (Client cli in clients)
				Console.WriteLine(cli);

			// Inverse l'ordre des éléments
			Console.WriteLine("\nInversion de l'ordre des éléments");
			clients.Reverse();
			foreach (Client cli in clients)
				Console.WriteLine(cli);
			#endregion
		}

		static void TesterDico_MéthodesCommunes()
		{
			Dictionary<string, Client> clients = new();
			clients.Add("EDUP", new Client("EDUP", "Dupont", "Eric"));
			clients.Add("PBEL", new Client("PBEL", "Belin", "Pauline"));
			clients.Add("LLEN", new Client("LLEN", "Lenoir", "Lucie"));
			clients.Add("ALEN", new Client("ALEN", "Lenoir", "Alain"));

			// Initialisation d'unn dictionnaire à partir d'un autre
			Dictionary<string, Client> clients2 = new(clients);

			// Accès à un élément particulier identifié par sa clé
			Console.WriteLine(clients["EDUP"].Nom);

			#region Accès aux éléments
			// Parcours des éléments
			foreach (KeyValuePair<string, Client> kvp in clients)
			{
				Console.WriteLine($"Client {kvp.Key} : {kvp.Value.Prénom} {kvp.Value.Nom}");
			}

			// Si on n'est pas sûr que le dico contient un élément avec une clé donnée :
			if (clients.TryGetValue("X", out Client? cliX))
				Console.WriteLine(cliX);
			#endregion

			#region Ajout et suppression d'éléments
			// Ajout d'un client s'il n'existe pas déjà (méthode commune aux différents dicos)
			Console.WriteLine("\nTeste si la clé existe déjà avant ajout :");
			if (!clients.ContainsKey("EDUP"))
				clients.Add("EDUP", new Client("EDUP", "Dupont", "Eric"));
			else
				Console.WriteLine("Un client EDUP existe déjà");


			// Suppression d'un élément
			Console.WriteLine("\nTentative de suppression d'un élément dont la clé n'existe pas");
			if (!clients.Remove("X"))
				Console.WriteLine("Le client X n'existe pas");

			if (clients.Remove("EDUP", out Client? cli))
			{
				Console.WriteLine($"Client {cli.Nom} retiré du dictionnaire");
			}
			#endregion

			#region Copie des valeurs dans un tableau
			Client[] tabClients = new Client[clients.Count];
			clients.Values.CopyTo(tabClients, 0);

			tabClients[2].Prénom = "Laurence";
			Console.WriteLine(clients["LLEN"]); // Laurence
			#endregion
		}

		static void TesterDico_MethodesSpécifiques()
		{
			Dictionary<string, Client> clients = new();
			clients.Add("EDUP", new Client("EDUP", "Dupont", "Eric"));
			clients.Add("PBEL", new Client("PBEL", "Belin", "Pauline"));
			clients.Add("LLEN", new Client("LLEN", "Lenoir", "Lucie"));
			clients.Add("ALEN", new Client("ALEN", "Lenoir", "Alain"));

			// Ajout dans un dictionnaire simple
			Console.WriteLine("\nTentative d'ajout d'un élément dont la clé existe déjà :");
			if (!clients.TryAdd("EDUP", new Client("EDUP", "Dupré", "Emmanuel")))
				Console.WriteLine("Un client EDUP existe déjà :");

			// Tri automatique des éléments par clé dans un dictionnaire trié
			Console.WriteLine("\nClients ajoutés dans le dictionnaire trié :");
			SortedDictionary<string, Client> dicoTrié = new(clients);
			foreach (KeyValuePair<string, Client> kvp in dicoTrié)
			{
				Console.WriteLine($"Client {kvp.Key} : {kvp.Value.Prénom} {kvp.Value.Nom}");
			}

			#region Liste triée
			SortedList<string, Client> listeTriée = new(clients);

			// Parcours comme un dictionnaire
			Console.WriteLine("\nParcours comme un dictionnaire :");
			foreach (var kvp in listeTriée)
			{
				Console.WriteLine($"Client {kvp.Key} : {kvp.Value.Prénom} {kvp.Value.Nom}");
			}

			// Parcours comme une liste
			Console.WriteLine("\nParcours comme une liste :");
			for (int c = 0; c < listeTriée.Count; c++)
			{
				Client cli = listeTriée.GetValueAtIndex(c);
				Console.WriteLine(cli);
			}

			// Récupère l'indice d'un élément à partir de sa clé :
			Console.Write("\nIndice de l'élément ayant la cle PBEL : ");
			Console.WriteLine(listeTriée.IndexOfKey("PBEL"));

			// Modifie l'élément situé à un indice donné
			listeTriée.SetValueAtIndex(2, new Client("BPEL", "Belin", "Paula"));

			// Supprime l'élément situé à un indice donné
			listeTriée.RemoveAt(2);
			#endregion
		}

		static void TesterListeChaînée()
		{
			LinkedList<Ville> trajet = new();

			trajet.AddFirst(new Ville("Amiens"));
			trajet.AddLast(new Ville("Paris"));
			trajet.AddLast(new Ville("Auxerre"));
			trajet.AddLast(new Ville("Beaune"));
			var repos = trajet.AddLast(new Ville("Lyon"));
			trajet.AddLast(new Ville("Avignon"));
			trajet.AddBefore(repos, new Ville("Mâcon"));
			trajet.AddAfter(repos, new Ville("Valence"));

			Console.WriteLine($"Trajet de {trajet.First?.Value.Nom} à {trajet.Last?.Value.Nom} " +
				$"avec repos entre {repos.Previous?.ValueRef.Nom} et {repos.Next?.ValueRef.Nom} :");

			trajet.Remove(repos);

			foreach (Ville lieu in trajet)
			{
				Console.Write($"{lieu.Nom}, ");
			}
			Console.WriteLine($"({trajet.Count - 2} étapes intermédiaires)");
		}

		static void TesterFile()
		{
			Queue<Etape> recette = new();
			recette.Enqueue(new Etape(1, "Peler et couper les pommes en fines tranches", 8));
			recette.Enqueue(new Etape(2, "Etaler la pâte sur un moule et la piquer avec une fourchette", 2));
			recette.Enqueue(new Etape(3, "Mélanger les oeufs, la crême et le sucre", 5));
			recette.Enqueue(new Etape(4, "Disposer les pommes et verser la préparation", 4));
			recette.Enqueue(new Etape(5, "Saupoudrer de cannelle", 1));
			recette.Enqueue(new Etape(6, "Enfourner à 180° pendant 35 min", 0));

			Console.WriteLine("Recette de tarte aux pommes : ");
			int durée = 0;
			foreach (Etape étape in recette)
			{
				Console.WriteLine($"{étape} ({étape.Durée}')");
				durée += étape.Durée;
			}
			Console.WriteLine($"\nTemps de préparation total : {durée} minutes");
			Console.WriteLine("\nAppuyez sur une touche pour commencer la recette.");
			Console.ReadKey(true);

			// Enlève le premier élément de la file tant qu'il y en a
			while (recette.TryDequeue(out Etape? étape))
			{
				Console.Clear();
				Console.WriteLine($"{étape} ({étape.Durée}')");
				Console.WriteLine("\nAppuyez sur une touche pour passer à l'étape suivante");
				Console.ReadKey(true);
			}
		}

		static void TesterPile()
		{
			Stack<Etape> construction = new();
			construction.Push(new Etape(1, "Dalle de béton"));
			construction.Push(new Etape(2, "Sous-sol"));
			construction.Push(new Etape(3, "Plancher principal"));
			construction.Push(new Etape(4, "Murs"));
			construction.Push(new Etape(5, "Plafond"));
			construction.Push(new Etape(6, "Toiture"));

			Console.WriteLine("Structure d'une maison :\n");
			do
			{
				foreach (Etape étape in construction)
					Console.WriteLine(étape);

				if (construction.Count > 0)
				{
					Console.WriteLine("\nAppuyez sur une touche pour détruire le niveau le plus haut");
					Console.ReadKey(true);
					Console.Clear();
				}
			}
			while (construction.TryPop(out Etape? e));
		}

		static void TesterEnsembles()
		{
			HashSet<Animal> chats = new()
			{
				new Animal(Especes.Chat, Sexes.Femelle, "Blanchette"),
				new Animal(Especes.Chat, Sexes.Femelle, "Zapette"),
				new Animal(Especes.Chat, Sexes.Femelle, "Choupette"),
				new Animal(Especes.Chat, Sexes.Male, "Ponpon"),
				new Animal(Especes.Chat, Sexes.Male, "Grison"),
				new Animal(Especes.Chat, Sexes.Male, "Riton")
			};

			HashSet<Animal> chiens = new() 
			{
				new Animal(Especes.Chien, Sexes.Femelle, "Rita"),
				new Animal(Especes.Chien, Sexes.Femelle, "Zita"),
				new Animal(Especes.Chien, Sexes.Femelle, "Zora"),
				new Animal(Especes.Chien, Sexes.Male, "Woolfy"),
				new Animal(Especes.Chien, Sexes.Male, "Rooky"),
				new Animal(Especes.Chien, Sexes.Male, "Skipy"),
			};

			HashSet<Animal> anx = new(); // ensemble vide
			anx.UnionWith(chats);   // ajout des chats
			anx.UnionWith(chiens);  // ajout des chiens
			AfficherContenu(anx); // affiche les 12 animaux

			// Enlève femelles
			anx.RemoveWhere(a => a.Sexe == Sexes.Femelle);
			AfficherContenu(anx); // Ponpon Grison Riton Woolfy Rooky Skipy

			// L'ensemble contient-il des chiens ?
			Console.WriteLine($"Contient des chiens ? {anx.Overlaps(chiens)}"); // True

			// L'ensemble est-il un sous-ensemble des chats
			Console.WriteLine($"Subset de chats ? {anx.IsSubsetOf(chats)}"); // false

			Console.WriteLine("Intersection avec l'ensemble des chats");
			anx.IntersectWith(chats); // ne garde que les chats
			Console.WriteLine($"Subset de chats ? {anx.IsSubsetOf(chats)}"); // true
			AfficherContenu(anx);   // Ponpon Grison Riton
		}

		static void AfficherContenu(HashSet<Animal> ens)
		{
			foreach (Animal a in ens)
			{
				Console.Write($"{a.Nom} ");
			}
			Console.WriteLine();
		}

		static void TesterRecords()
		{
			Produit p1 = new Produit(1, "sac à dos");
			Produit p2 = new Produit(1, "sac à dos");

			Console.WriteLine(p1); // Produit { Id = 1, Libellé = sac à dos }
			Console.WriteLine(p1 == p2); // true

			// récupère les valeurs de propriétés sous forme de tuple
			(int id, string lib) = p1;

			Produit p3 = p1 with { }; // Copie toutes les valeurs de propriétés à l'identique
			Produit p4 = p1 with { Libellé = "sac à dos 30L" }; // Copie en changeant une propriété
		}
	}
}