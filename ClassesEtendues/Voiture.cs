using System.Xml.Serialization;

namespace ClassesEtendues
{
	public class Voiture
	{
		[XmlAttribute("Id")]
		public int Id { get; set; }

		[XmlElement("Marque")]
		public string Marque { get; set; } = string.Empty;

		[XmlElement("Modele")]
		public string Modèle { get; set; } = string.Empty;

		[XmlElement("Annee")]
		public int Année { get; set; }
	}
}
