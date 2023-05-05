using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Interfaces_DI
{
	internal enum ModesNotif { SMS, Email }
	internal class Program
	{
		public static ServiceProvider? ServiceProvider;

		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Unicode;
			TesterService();
			Console.WriteLine();

			CreerConteneurIoC(ModesNotif.Email);

			TesterConteneurIoC();
		}

		private static void TesterService()
		{
			DateOnly dateOuv = new DateOnly(2023, 1, 1);
			decimal soldeInit = 100m;
			var comptes = new CompteBancaire[2];

			// Compte sans notifications
			comptes[0] = new CompteBancaire(1111, dateOuv, soldeInit);

			// Compte avec notifications
			INotifier noti = new NotificateurSMS();
			comptes[1] = new CompteBancaire(2222, dateOuv, soldeInit, noti);

			foreach (CompteBancaire cb in comptes)
			{
				cb.Crediter(200m, MoyensTransaction.Virement, "Crédit");
				cb.Debiter(400m, MoyensTransaction.Carte, "Débit");
			}
		}

		static void CreerConteneurIoC(ModesNotif modeNotif)
		{
			ServiceCollection services = new();

			// Enregistre les services dans le conteneur IoC
			if (modeNotif == ModesNotif.Email)
				services.AddSingleton<INotifier, NotificateurEmail>();
			else
				services.AddSingleton<INotifier, NotificateurSMS>();

			services.AddTransient<IEmailSender, EmailSender>();

			// Construit le conteneur IoC
			ServiceProvider = services.BuildServiceProvider(true);
		}

		private static void TesterConteneurIoC()
		{
			if (ServiceProvider == null)
				throw new InvalidOperationException();

			// Récupère une instance de service de notification depuis le conteneur
			INotifier noti = ServiceProvider.GetRequiredService<INotifier>();

			// Crée un compte bancaire utilisant ce service
			DateOnly dateOuv = new DateOnly(2023, 1, 1);
			decimal soldeInit = 100m;
			var cb = new CompteBancaire(2222, dateOuv, soldeInit, noti);

			// Uitilise le compte
			cb.Crediter(200m, MoyensTransaction.Virement, "Crédit");
			cb.Debiter(400m, MoyensTransaction.Carte, "Débit");
		}
	}
}