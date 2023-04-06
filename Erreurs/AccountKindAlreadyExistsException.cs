namespace Erreurs
{
	public class AccountKindAlreadyExistsException : Exception
	{
		public long AccountNumber { get; set; }

		public AccountKindAlreadyExistsException()
		{

		}

		public AccountKindAlreadyExistsException(string message) : base(message)
		{

		}

		public AccountKindAlreadyExistsException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
