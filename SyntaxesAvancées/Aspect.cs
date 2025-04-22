namespace SyntaxesAvancées;

public enum Orientations { Carrée, Horizontale, Verticale }

/// <summary>
/// Modélise l'aspect d'une image (ratio Largeur / Hauteur et orientation)
/// </summary>
public class Aspect
{
	// Propriétés
	public string Code { get; }
	public double RatioLH { get; }
	public Orientations Orientation { get; }

	// Constructeur
	public Aspect(string code)
	{
		Code = code;

		switch (code)
		{
			case "A4H":
				RatioLH = 29.7 / 21;
				Orientation = Orientations.Horizontale;
				break;
			case "A4V":
				RatioLH = 21 / 29.7;
				Orientation = Orientations.Verticale;
				break;
			case "C":
				RatioLH = 1;
				Orientation = Orientations.Carrée;
				break;
			default:
				throw new FormatException("Code d'aspect non reconnu : " + code);
		}
	}
}

public class Aspect2
{
	public string Code { get; }
	public double RatioLH { get; }
	public Orientations Orientation { get; }

	// Constructeur
	public Aspect2(string code)
	{
		Code = code;

		RatioLH = code switch
		{
			"A4H" => 29.7 / 21,
			"A4V" => 21 / 29.7,
			"C" => 1,
			_ => throw new FormatException("Code d'aspect non reconnu : " + code)
		};

		Orientation = RatioLH switch
		{
			< 1 => Orientations.Verticale,
			> 1 => Orientations.Horizontale,
			_ => Orientations.Carrée,
		};
	}

	public override string ToString() => Code switch
	{
		"A4H" => "image A4 orientée horizontalement",
		"A4V" => "image A4 orientée verticalement",
		"C" => "image carrée",
		_ => "image d'aspect indéterminé"
	};
}

						  