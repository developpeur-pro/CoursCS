using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
	internal class CompteBancaire
	{
		private const decimal PLAFOND_MAXI = 1000m;
		private static decimal _tauxAgios;

		#region Propriétés publiques
		public long Numero { get; }
		public DateOnly DateCreation { get; init; }
		public string Libelle { get; set; } = string.Empty;
		public decimal Solde { get; private set; }
		#endregion

		#region Constructeurs
		public CompteBancaire()
		{

		}

		public CompteBancaire(long numero)
		{
			Numero = numero;
			DateCreation = DateOnly.FromDateTime(DateTime.Today);
			Libelle = $"Compte N°{numero}";
		}

		public CompteBancaire(long numero, DateOnly dateCreation) : this(numero)
		{
			DateCreation = dateCreation;
		}

		public CompteBancaire(long numero, DateOnly dateCreation, string libelle) :
			this(numero, dateCreation)
		{
			Libelle = libelle;
		}
		#endregion

		#region Méthodes publiques (accessibles à l’extérieur de la classe)
		public void Crediter(decimal montant)
		{
			Solde += montant;
		}

		// Méthode non statique
		public void Debiter(decimal montant)
		{
			Solde -= montant;
			if (Solde < 0)
				Solde -= Solde * _tauxAgios; // Utilisation du champ statique OK
		}

		public void Debiter(decimal montant, string libelle)
		{
			Debiter(montant);
			// ...code supplémentaire gérant le libellé
		}

		public void Debiter(decimal montant, string libelle, DateOnly dateEffet)
		{
			Debiter(montant, libelle);
			// ...code supplémentaire gérant la date d'effet
		}

		// Méthode statique
		public static void ModifierTauxAgios(decimal taux)
		{
			_tauxAgios = taux; // accès au champ statique OK
			//Libelle = "COMPTE"; // accès à la propriété non statique pas OK
		}
		#endregion

		protected virtual decimal CalculerInterets()
		{
			decimal interets = Solde * 0.005m;
			Solde += interets;
			return interets;
		}
	}

	public class Client
	{
		public required string Nom { get; set; } = string.Empty;
		public required string Prenom { get; set; } = string.Empty;

		public string NomComplet => $"{Nom} {Prenom}";
	}
}