using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxesAvancées;

public abstract class CompteBancaire
{
	public long Numero { get; }
	public DateOnly DateOuverture { get; init; }

	public CompteBancaire(long numero)
	{
		Numero = numero;
	}

	public CompteBancaire(long numero, DateOnly dateOuv) : this(numero)
	{
		DateOuverture = dateOuv;
	}
}

public class CompteCourant : CompteBancaire
{
	public decimal Solde { get; set; }

	public CompteCourant(long numero) : base(numero) { }
}

public class CompteEpargne : CompteBancaire
{
	public decimal Solde { get; set; }

	public CompteEpargne(long numero) : base(numero) { }
}

public class CompteTitres : CompteBancaire
{
	public decimal ValeurTitres { get; set; }

	public CompteTitres(long numero) : base(numero) { }
}
