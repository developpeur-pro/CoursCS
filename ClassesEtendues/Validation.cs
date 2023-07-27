using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ClassesEtendues
{
	public class ValidationBase
	{
		/// <summary>
		/// Vérifie la validité d'une valeur de propriété à partir des attributs qui la décorent
		/// </summary>
		/// <param name="value">Valeur à valider</param>
		/// <param name="propertyName">Nom de la propriété si pas déterminé automatiquement</param>
		/// <exception cref="ValidationException">valeur de propriété non valide</exception>
		public void ValidateProperty(object value, [CallerMemberName] string? propertyName = null)
		{
			ValidationContext context = new(this);
			context.MemberName = propertyName;
			Validator.ValidateProperty(value, context);
			// La ligne précédente lève une ValidationException si la valeur n'est pas valide
		}
	}

	public class CompteUtil : ValidationBase
	{
		private string _pseudo = string.Empty;
		private string _email = string.Empty;
		private string _motDePasse = string.Empty;

		public CompteUtil()
		{
		}

		public CompteUtil(string pseudo, string email, string motDePasse)
		{
			Pseudo = pseudo;
			Email = email;
			MotDePasse = motDePasse;
		}

		[StringLength(20, MinimumLength = 5, ErrorMessage = "le pseudo doit faire entre 5 et 20 caractères")]
		[CustomValidation(typeof(RèglesValidation), nameof(RèglesValidation.PseudoDispo))]
		public string Pseudo
		{
			get => _pseudo;
			set
			{
				ValidateProperty(value);
				_pseudo = value;
			}
		}

		[Required(ErrorMessage = "L'email est obligatoire")]
		[EmailAddress(ErrorMessage = "Format d'email incorrect")]
		public string Email
		{
			get => _email;
			set
			{
				ValidateProperty(value);
				_email = value;
			}
		}

		[MinLength(6)]
		[RegularExpression(@".*[A-Z]+.*[0-9]+.*", ErrorMessage = "Le mot de passe doit contenir au moins une majuscule et un chiffre")]
		public string MotDePasse
		{
			get => _motDePasse;
			set
			{
				ValidateProperty(value);
				_motDePasse = value;
			}
		}
	}

	public class RèglesValidation
	{
		public static ValidationResult? PseudoDispo(string value)
		{
			// TODO : code pour vérifier si le pseudo n'est pas déjà prise
			//	return new ValidationResult("Pseudo non disponible");

			return ValidationResult.Success;
		}
	}
}
