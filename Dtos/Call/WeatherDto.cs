using Newtonsoft.Json;

public class WeatherDto
{
    [JsonProperty("main")]
    public MainWeather Main { get; set; }

    [JsonProperty("weather")]
    public Weather[] Weather { get; set; }

    [JsonProperty("sys")]
    public Sys Sys { get; set; }
}

public class MainWeather
{
    [JsonProperty("temp")]
    public double Temp { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }
}

public class Weather
{
    [JsonProperty("description")]
    public string Description { get; set; }
}

public class Sys
{
    [JsonProperty("country")]
    public string Country { get; set; }
}

public class CountryDto
{
    [JsonProperty("name")]
    public Name Name { get; set; }

    [JsonProperty("population")]
    public long Population { get; set; }

    [JsonProperty("languages")]
    public Dictionary<string, string> Languages { get; set; }

    [JsonProperty("currencies")]
    public Dictionary<string, Currency> Currencies { get; set; }
}

public class Name
{
    [JsonProperty("common")]
    public string Common { get; set; }

    [JsonProperty("official")]
    public string Official { get; set; }
}

public class Currency
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}
