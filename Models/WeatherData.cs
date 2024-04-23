using System;
using System.Collections.Generic;

namespace Models;
public class Geometry
{
    public List<double> coordinates { get; set; }
    public string type { get; set; }
}

public class Units
{
    public string air_pressure_at_sea_level { get; set; }
    public string air_temperature { get; set; }
    public string cloud_area_fraction { get; set; }
    public string precipitation_amount { get; set; }
    public string relative_humidity { get; set; }
    public string wind_from_direction { get; set; }
    public string wind_speed { get; set; }
}

public class Meta
{
    public Units units { get; set; }
    public DateTime updated_at { get; set; }
}

public class Details
{
    public double? air_pressure_at_sea_level { get; set; }
    public double? air_temperature { get; set; }
    public string? cloud_area_fraction { get; set; }
    public string? relative_humidity { get; set; }
    public string? wind_from_direction { get; set; }
    public string? wind_speed { get; set; }
    public string? precipitation_amount { get; set; }
    // Add other properties as needed
}

public class Instant
{
    public Details details { get; set; }
}

public class Summary
{
    public string symbol_code { get; set; }
}

public class Next1Hours
{
    public Details details { get; set; }
    public Summary summary { get; set; }
}

public class Next6Hours
{
    public Details details { get; set; }
    public Summary summary { get; set; }
}

public class Data
{
    public Instant instant { get; set; }
    public Next1Hours next_1_hours { get; set; }
    public Next6Hours next_6_hours { get; set; }
    // Add other properties as needed
}

public class TimeSeries
{
    public Data data { get; set; }
    public DateTime time { get; set; }
}

public class Properties
{
    public Meta meta { get; set; }
    public List<TimeSeries> timeseries { get; set; }
}

public class WeatherResponse
{
    public Geometry geometry { get; set; }
    public Properties properties { get; set; }
}
