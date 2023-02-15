using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bases
{
	/// <summary>
	/// Fournit des méthodes de calcul sur des points du plan
	/// </summary>
	internal class CalculettePlan
	{
		/// <summary>
		/// Calcule et renvoie la distance entre 2 points
		/// </summary>
		/// <param name="x1">abscisse du premier point</param>
		/// <param name="y1">ordonnée du premier point</param>
		/// <param name="x2">abscisse du second point</param>
		/// <param name="y2">ordonnée du second point</param>
		/// <returns>distance entre les 2 points</returns>
		public static double GetDistance(double x1, double y1, double x2, double y2)
		{
			return Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));
		}
	}
}
