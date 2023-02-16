using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace DatesDurées
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Unicode;

			DateTime dtLocale = new DateTime(2030, 3, 30, 23, 0, 0);
			TimeSpan heuresAjoutées = new TimeSpan(7, 0, 0);

			// Récupère la zone de temps locale dans les paramètres système
			TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Australia/Sydney"); //TimeZoneInfo.Local;

			// ...et son décalage horaire par rapport au temps UTC
			TimeSpan décalage = tz.GetUtcOffset(dtLocale);

			// Stocke la date/heure locale + son décalage dans un DateTimeOffset
			DateTimeOffset dto = new DateTimeOffset(dtLocale, décalage);

			// Convertit cette date/heure en temps UTC et lui ajoute les heures souhaitées
			DateTimeOffset dto2 = dto.ToUniversalTime().Add(heuresAjoutées);

			// Reconvertit le résultat en temps local
			dto2 = TimeZoneInfo.ConvertTime(dto2, tz);

			// Affiche le résultat
			Console.WriteLine($"{dto} + {heuresAjoutées:h'h'} = {dto2}");
			// 30/03/30 23:00:00 +01:00 + 7h = 31/03/30 07:00:00 +02:00

			Console.ReadKey();
		}

		static void DemoAge()
		{
			while (true)
			{
				bool repOk;
				DateTime dateNais;
				do
				{
					Console.WriteLine("Quelle est votre date de naissance (JJ/MM/AAAA) ?");
					string? rep = Console.ReadLine();
					repOk = DateTime.TryParse(rep, out dateNais);
				} while (!repOk);

				Console.WriteLine(ToAgeString(dateNais));
				//Console.WriteLine(CalculerAge(dateNais));
			}
		}

		static string CalculerAge(DateTime dateNais)
		{
			// Nombre d'années
			int ans = DateTime.Today.Year - dateNais.Year;
			if (DateTime.Today.Month < dateNais.Month ||
				(DateTime.Today.Month == dateNais.Month && DateTime.Today.Day < dateNais.Day))
				ans--;

			// Nombre de mois 
			int mois = DateTime.Today.Month - dateNais.Month;

			if (mois < 0)
				mois = 12 - dateNais.Month + DateTime.Today.Month;

			return $"{ans} ans et {mois} mois";
		}

		static string ToAgeString(DateTime dateNais)
		{
			DateTime today = DateTime.Today;

			int nbJours = today.Day - dateNais.Day;
			int nbMois = today.Month - dateNais.Month;
			int nbAns = today.Year - dateNais.Year;

			if (nbJours < 0)
			{
				nbMois--;
			}

			if (nbMois < 0)
			{
				nbAns--;
				nbMois += 12;
			}

			DateTime dateTemp =  dateNais.AddMonths((nbAns * 12) + nbMois);
			nbJours = (today - dateTemp).Days;

			return $"{nbAns} an(s), {nbMois} mois et {nbJours} jour(s)";
		}
	}
}