namespace SyntaxesAvancées;
/*
// Classe abstraite représentant les modules et leçons d'une formation
internal abstract class ItemFormation
{
	public short Id { get; }
	public string Titre { get; }

	public ItemFormation(short id, string titre)
	{
		Id = id;
		Titre = titre;
	}
}

// Leçon
internal class Leçon : ItemFormation
{
	public Leçon(short id, string titre) : base(id, titre) { }
}

// Module contenant d'autres items (modules ou leçons)
internal class Module : ItemFormation
{
	public Module(short id, string titre) : base(id, titre)
	{
		Items = new List<ItemFormation>();
	}

	public List<ItemFormation> Items { get; }
}
*/

// Classe abstraite pour les modules et leçons de la formation
internal abstract class ItemFormation(short id, string titre)
{
	public short Id { get; } = id;
	public string Titre { get; } = titre;
}

// Module contenant d'autres items (modules ou leçons)
internal class Module(short id, string titre) : ItemFormation(id, titre)
{
	public List<ItemFormation> Items { get; } = new();
}

// Leçon
internal class Leçon(short id, string titre) : ItemFormation(id, titre)
{
	public short NiveauDifficulte { get; set; }

	public Leçon(short id, string titre, short niveau) : this(id, titre)
	{
		NiveauDifficulte = niveau;
	}
}

internal interface INotifier { }
// Quiz
internal class Quiz(short id, string titre, INotifier notifieur) : ItemFormation(id, titre)
{
	 //private INotifier _notifieur = notifieur; // inutile
}


internal class Formation
{
	public List<ItemFormation> Items { get; }

	// Initialise les modules et leçons de la formation
	public Formation()
	{
		Items = new();

		// Premier module avec 3 leçons
		var module1 = new Module(1, "Module 1");
		module1.Items.Add(new Leçon(1, "Leçon 1"));
		module1.Items.Add(new Leçon(2, "Leçon 2"));
		module1.Items.Add(new Leçon(3, "Leçon 3"));
		Items.Add(module1);

		// Deuxième module avec 4 leçons
		var module2 = new Module(2, "Module 2");
		module2.Items.Add(new Leçon(1, "Leçon 1"));
		module2.Items.Add(new Leçon(2, "Leçon 2"));
		module2.Items.Add(new Leçon(3, "Leçon 3"));
		module2.Items.Add(new Leçon(4, "Leçon 4"));
		Items.Add(module2);

		// Troisième module avec 3 sous-modules contenant chacun 3 leçons
		var module3 = new Module(3, "Module 3");
		for (short i = 1; i <= 3; i++)
		{
			var sousModule = new Module(i, $"Sous-module {i}");
			for (short j = 1; j <= 3; j++)
			{
				sousModule.Items.Add(new Leçon(j, $"Leçon {j}"));
			}
			module3.Items.Add(sousModule);
		}
		Items.Add(module3);
	}

	// Construit et renvoie le plan de la formation sous forme d'un dico dont les clés sont
	// les identifiants complets des items (de la forme 3.1.2) et les valeurs les titres des items
	public Dictionary<string, string> ConstruirePlan()
	{
		var plan = new Dictionary<string, string>();

		// Initie le traitement récursif
		ConstruirePlanRecursif(Items, null, plan);
		return plan;

		// Méthode récursive imbriquée
		void ConstruirePlanRecursif(List<ItemFormation> items, string? idParentComplet, Dictionary<string, string> itemIds)
		{
			foreach (ItemFormation item in items)
			{
				itemIds.Add(idParentComplet + item.Id, item.Titre);
				if (item is Module module)
				{
					ConstruirePlanRecursif(module.Items, idParentComplet + module.Id + ".", itemIds);
				}
			}
		}
	}
}
