using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fonctions
{
	/// <summary>
	/// Fournit des fonctions mathématiques supplémentaires par rapport à celles de .Net
	/// </summary>
	internal class OutilsMaths
	{
		public static double GetValeurMaxi(params double[] nombres)
		{
			double max = double.MinValue;
			for (int i = 0; i < nombres.Length; i++)
			{
				if (nombres[i] > max) max = nombres[i];
			}

			return max;
		}

		/// <summary>
		/// Renvoie les valeurs min et max d'un ensemble de nombres passés en paramètres
		/// </summary>
		/// <param name="nombres">les nombres à analyser </param>
		/// <returns>tuple de valeurs min et max</returns>
		public static (double, double) GetValeursMinMax(params double[] nombres)
		{
			double min = double.MaxValue;
			double max = double.MinValue;
			foreach (double nombre in nombres)
			{
				if (nombre > max) max = nombre;
				if (nombre < min) min = nombre;
			}

			return (min, max);
		}

		public static void GetValeursMinMax(out double min, out double max, params double[] nombres)
		{
			min = double.MaxValue;
			max = double.MinValue;
			foreach (double nombre in nombres)
			{
				if (nombre > max) max = nombre;
				if (nombre < min) min = nombre;
			}
		}
	}
}
