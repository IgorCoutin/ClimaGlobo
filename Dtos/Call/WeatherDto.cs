using Newtonsoft.Json;

public class WeatherDto
{
    [JsonProperty("coord")]
    public Coordinates Coord { get; set; }
}

public class Coordinates
{
    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("lon")]
    public double Lon { get; set; }
}
