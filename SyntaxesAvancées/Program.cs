using SyntaxesAvancées;
using System.Diagnostics;
using System.Text;

Console.OutputEncoding = Encoding.Unicode;

//TestSwitch.TestValeurs();
//TestSwitch.TestTypes();

//TestIntervalle.TestRange();
//TestIntervalle.TestOperateurSurTableau();
//TestIntervalle.TestOperateurSurListe();
//TestIntervalle.TestAssemblageTableaux();

//TestIntervalle.TestExtractionChaine();
//TestIntervalle.TestOperateurIndiceFin();

//TestRegex.TestIsMatch();
//TestRegex.TestMatch();
//TestRegex.TestMatches();
//TestRegex.TestReplace();
//TestRegex.TestReplace2();

Formation formation = new();
Dictionary<string, string> plan = formation.ConstruirePlan();
foreach(var kvp in plan)
{
	Console.WriteLine(kvp.Key + " " + kvp.Value);
}