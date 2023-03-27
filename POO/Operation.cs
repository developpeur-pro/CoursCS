using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
	public class Operation
	{
		public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
		public decimal Montant { get; set; }
		public string Libelle { get; set; } = string.Empty;

		public Operation(decimal montant)
		{
			Montant = montant;
		}
	}
}
