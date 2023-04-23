namespace Interfaces_DI
{
	#region Interfaces
	public interface INotifier
	{
		// Historique des notifs
		Queue<INotification> Notifications { get; }

		// Nb maxi d'éléments à conserver dans l'historique
		int NbNotifsMaxi { get; }

		// Envoi d'une notif
		void Notifier(INotification notif);
	}

	public interface INotification
	{
		DateTime Date { get; init; }
		string Message { get; init; }
	}

	public interface IEmailSender
	{
	}
	#endregion

	#region Implémentations
	public abstract class Notificateur : INotifier
	{
		public Queue<INotification> Notifications { get; } = new();
		public int NbNotifsMaxi { get; set; } = 3;

		public virtual void Notifier(INotification notif)
		{
			if (Notifications.Count == NbNotifsMaxi)
				Notifications.Dequeue();

			Notifications.Enqueue(notif);
		}
	}

	public class NotificateurSMS : Notificateur
	{
		public override void Notifier(INotification notif)
		{
			base.Notifier(notif);
			Console.WriteLine($"Envoi d'un SMS le {notif.Date} : {notif.Message}");
		}
	}

	public class NotificateurEmail : Notificateur
	{
		private IEmailSender _mailer;
		public NotificateurEmail(IEmailSender mailer)
		{
			_mailer = mailer;
		}

		public override void Notifier(INotification notif)
		{
			base.Notifier(notif);
			Console.WriteLine($"Envoi d'un email le {notif.Date} : {notif.Message}");
			
			// ... + code d'envoi d'une email via le mailer
		}
	}

	public class Notification : INotification
	{
		public DateTime Date { get; init; }
		public string Message { get; init; }

		public Notification(DateTime date, string message)
		{
			Date = date;
			Message = message;
		}
	}

	public class EmailSender : IEmailSender
	{
		public EmailSender()
		{
			Console.WriteLine("Création d'un service d'envoi d'emails");
		}
	}
	#endregion
}
