
Console.WriteLine("Bonjour ! Comment t'appeles-tu ?");
string? rep = Console.ReadLine();

Console.WriteLine("Bonjour " + rep + " !");

Console.WriteLine("Quelle est ton année de naissance ?");
rep = Console.ReadLine();

int annéeNais = int.Parse(rep);

const int annéeMin = 1900;
if (annéeNais >= annéeMin && annéeNais <= DateTime.Today.Year)
{
	int age = DateTime.Today.Year - annéeNais;
	Console.WriteLine($"Tu as ou vas avoir {age} ans en {DateTime.Today.Year}");

	string tranche = string.Empty;
	
	//if (age < 25)
	//	tranche = "moins de 25 ans";
	//else if (age >= 25 && age < 40)
	//	tranche = "25 - 40 ans";
	//else if (age >= 40 && age < 55)
	//	tranche = "40 - 55 ans";
	//else
	//	tranche = "plus de 55 ans";

	switch (age)
	{
		case < 25:
			tranche = "moins de 25 ans";
			break;

		case < 40:
			tranche = "25 - 40 ans";
			break;

		case < 55:
			tranche = "40 - 55 ans";
			break;

		default:
			tranche = "plus de 55 ans";
			break;
	}

	Console.WriteLine($"Ta tranche d'âge est : {tranche}");
}
else
{
	Console.WriteLine($"Ton année de naissance doit être comprise entre {annéeMin} et {DateTime.Today.Year}");
}

Console.WriteLine("As-tu un code de réduction (O/N) ?");
rep = Console.ReadLine();
bool reduc = (rep == "O");

Console.WriteLine(annéeNais < 2000 ? "Tu est né au 20ème siècle" : "Tu es née au 21ème sicèle");


// --------------------------------------------------------
// Les tableaux

// Stokage des résultats d'un match de tennis
byte?[] scoresJ1 = new byte?[5];
byte?[] scoresJ2 = { 2, 6, 7, 3, 5 };

scoresJ1[0] = 6;
scoresJ1[1] = 3;
scoresJ1[2] = 5;
scoresJ1[3] = 6;
scoresJ1[4] = 7;

scoresJ2[0] = 3;

Console.WriteLine(scoresJ2.Length); // 5

string[] courses = { "chou", "riz", "cacao" };

string phrase = "Le C# est un langage moderne et puissant !";

char car = phrase[4];
//phrase[2] = 'a';

byte?[,] scores = new byte?[2, 5];
scores[0, 0] = 6;
scores[1, 0] = 2;
scores[0, 1] = 3;
scores[1, 1] = 6;
scores[0, 2] = 5;
scores[1, 2] = 7;

byte?[,] scores2 = { { 6, 2 }, { 3, 6 }, { 5, 7 }, { 6, 3 }, { 7, 5 } };

// --------------------------------------------------------
// Itérations

for (int s = 0; s < scoresJ1.Length; s++)
{
	Console.WriteLine($"set {s + 1} : {scoresJ1[s]} - {scoresJ2[s]}");
}

Console.WriteLine();

for (int s = scoresJ1.Length - 1; s >= 0; s--)
{
	Console.WriteLine($"set {s + 1} : {scoresJ1[s]} - {scoresJ2[s]}");
}

Console.WriteLine();

int premierSetPerdu = 0;
for (int s = 0; s < scoresJ1.Length; s++)
{
	if (scoresJ1[s] < scoresJ2[s])
	{
		premierSetPerdu = s + 1;
		break;
	}
}
Console.WriteLine($"N° du premier set perdu par le joueur 1 : {premierSetPerdu}");

Console.WriteLine("Sets perdus par le joueur 1 :");
for (int s = 0; s < scoresJ1.Length; s++)
{
	if (scoresJ1[s] > scoresJ2[s])
		continue;

	Console.WriteLine($"set {s + 1}");
}

// Instruction foreach
int nbJeux = 0;
foreach (byte score in scoresJ1)
{
	nbJeux += score;	
}

Console.WriteLine($"Le premier joueur a remporté {nbJeux} jeux");

// Instruction do...while
bool saisieOK = false;
do
{
	Console.WriteLine("Saisissez un chiffre entre 1 et 3");
	string? rep2 = Console.ReadLine();
	if (rep == "1" || rep == "2" || rep == "3")
		saisieOK = true;

} while (!saisieOK);

// Instruction while
while (!saisieOK)
{
	Console.WriteLine("Saisissez un chiffre entre 1 et 3");
	string? rep2 = Console.ReadLine();
	if (rep == "1" || rep == "2" || rep == "3")
		saisieOK = true;
}