using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgAsychrone
{
	public class WebAPI
	{
		private static readonly HttpClient client = new();

		// Méthode synchrone
		public static HeuresSoleil? GetHeuresSoleil(double lat, double lng, int num)
		{
			string url = $"https://api.sunrisesunset.io/json?lat={lat}&lng={lng}".Replace(',', '.');

			// Envoie une requête et attend la réponse
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage reponse = client.Send(req);
			Console.WriteLine($"\nRéponse requête {num} reçue");

			// Extrait le flux JSON de la réponse
			reponse.EnsureSuccessStatusCode();
			Stream stream = reponse.Content.ReadAsStream();
			Console.WriteLine($"\nTexte JSON {num} extrait");

			// Desérialise le flux JSON
			JsonSerializerOptions options = new()
			{
				Converters = { new TimeOnlyJsonConverter() }
			};
			Root? result = JsonSerializer.Deserialize<Root>(stream, options);
			Console.WriteLine($"\nFlux JSON {num} désérialisé");

			return result?.Heures;
		}

		// Tâche asynchrone 
		public static async Task<HeuresSoleil?> GetHeuresSoleilAsync(double lat, double lng, int num)
		{
			string url = $"https://api.sunrisesunset.io/json?lat={lat}&lng={lng}".Replace(',', '.');

			// Exécute la requête, attend son résultat 
			// et rend le contrôle à l'appelant en attendant
			Console.WriteLine($"\nRequête {num} envoyée");
			HttpResponseMessage reponse = await client.GetAsync(url);
			Console.WriteLine($"\nRéponse requête {num} reçue");

			reponse.EnsureSuccessStatusCode();
			// extrait le texte JSON de la réponse 
			// et rend le contrôle à l'appelant en attendant
			string txt = await reponse.Content.ReadAsStringAsync();
			Console.WriteLine($"\nTexte JSON {num} extrait");

			// Desérialise le flux JSON
			JsonSerializerOptions options = new()
			{
				Converters = { new TimeOnlyJsonConverter() }
			};
			Root? result = JsonSerializer.Deserialize<Root>(txt, options);
			Console.WriteLine($"\nFlux JSON {num} désérialisé");

			return result?.Heures;
		}

		// Tâche asynchrone annulable
		// Reçoit un jeton d'annulation en paramètre et l'utilise dans l'exécution des tâches
		public static async Task<HeuresSoleil?> GetHeuresSoleilAsync(double lat, double lng, int num, CancellationToken jetonAnnul)
		{
			string url = $"https://api.sunrisesunset.io/json?lat={lat}&lng={lng}".Replace(',', '.');

			// Exécute la requête, attend son résultat 
			// et rend le contrôle à l'appelant en attendant, avec possibilité d'annulation
			Console.WriteLine($"\nRequête {num} envoyée");
			HttpResponseMessage reponse = await client.GetAsync(url, jetonAnnul);
			Console.WriteLine($"\nRéponse requête {num} reçue");

			reponse.EnsureSuccessStatusCode();
			// Ajoute un délai de 4s pour avoir le temps d'annuler la tâche (juste pour la démo)
			await Task.Delay(4000, jetonAnnul);
			//jetonAnnul.ThrowIfCancellationRequested();

			// extrait le texte JSON de la réponse 
			// et rend le contrôle à l'appelant en attendant, avec possibilité d'annulation
			string txt = await reponse.Content.ReadAsStringAsync(jetonAnnul);
			Console.WriteLine($"\nTexte JSON {num} extrait");

			// Desérialise le flux JSON
			JsonSerializerOptions options = new()
			{
				Converters = { new TimeOnlyJsonConverter() }
			};
			Root? result = JsonSerializer.Deserialize<Root>(txt, options);
			Console.WriteLine($"\nFlux JSON {num} désérialisé");

			return result?.Heures;
		}
	}

	public class Root
	{
		[JsonPropertyName("results")]
		public HeuresSoleil? Heures { get; set; }
		[JsonPropertyName("status")]
		public string Statut { get; set; } = string.Empty;
	}

	public record class HeuresSoleil
	{
		[JsonPropertyName("first_light")]
		public TimeOnly PremiereLumiere { get; set; }

		[JsonPropertyName("dawn")]
		public TimeOnly Aube { get; set; }

		[JsonPropertyName("sunrise")]
		public TimeOnly LeverDuSoleil { get; set; }

		[JsonPropertyName("solar_noon")]
		public TimeOnly MidiSolaire { get; set; }

		[JsonPropertyName("golden_hour")]
		public TimeOnly HeureDoree { get; set; }

		[JsonPropertyName("sunset")]
		public TimeOnly CoucherDuSoleil { get; set; }

		[JsonPropertyName("dusk")]
		public TimeOnly Crepuscule { get; set; }

		[JsonPropertyName("last_light")]
		public TimeOnly DerniereLumiere { get; set; }

		[JsonPropertyName("day_length")]
		public TimeSpan DureeDuJour { get; set; }

		[JsonPropertyName("timezone")]
		public string FuseauHoraire { get; set; } = string.Empty;

		[JsonPropertyName("utc_offset")]
		public int DecalageUtc { get; set; }
	}

	public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
	{
		public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return TimeOnly.Parse(reader.GetString()!, CultureInfo.GetCultureInfo("en-US"));
		}

		public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
