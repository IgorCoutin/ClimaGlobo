using Newtonsoft.Json;
using System.Collections.Generic;

public class CallDto
{
    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("lon")]
    public double Lon { get; set; }

    [JsonProperty("current")]
    public CurrentWeather Current { get; set; }

    [JsonProperty("hourly")]
    public List<HourlyWeather> Hourly { get; set; }

    [JsonProperty("daily")]
    public List<DailyWeather> Daily { get; set; }
}

public class CurrentWeather
{
    public double Temp { get; set; }
    public double Humidity { get; set; }
    public double Wind_Speed { get; set; }
    public Weather[] Weather { get; set; }
}

public class HourlyWeather
{
    public long Dt { get; set; }  
    public double Temp { get; set; }  
    public int Humidity { get; set; }     public double Wind_Speed { get; set; }  
    public Weather[] Weather { get; set; } 
}

public class DailyWeather
{
    public long Dt { get; set; }
    public Temp Temp { get; set; }
    public Weather[] Weather { get; set; }
}

public class Temp
{
    public double Day { get; set; }
    public double Night { get; set; }
   
}

public class Weather
{
    public string Description { get; set; }
}
