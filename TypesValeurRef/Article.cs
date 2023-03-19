using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesValeurRef
{
	internal class Article
	{
		public int Id { get; }
		public string Libelle { get; set; }
		public Fournisseur? Fournisseur { get; set; }


		public Article(int id, string libelle)
		{
			Id = id;
			Libelle = libelle;
		}
	}

	internal class Fournisseur
	{
		public long SIRET { get; set; }
		public string? Nom { get; set; } = string.Empty;

		public Fournisseur(long siret, string? nom)
		{
			SIRET = siret;
			Nom = nom;
		}
	}
}
