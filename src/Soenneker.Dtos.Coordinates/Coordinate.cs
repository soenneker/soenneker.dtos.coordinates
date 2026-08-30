using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Soenneker.Dtos.Coordinates;

/// <summary>
/// Identifies a geographic point using WGS84 latitude and longitude expressed in decimal degrees.
/// </summary>
public readonly struct Coordinate : ISpanFormattable
{
    /// <summary>
    /// Latitude in decimal degrees, from -90 at the South Pole through 90 at the North Pole.
    /// </summary>
    [JsonPropertyName("latitude")]
    [JsonProperty("latitude")]
    public double Latitude { get; init; }

    /// <summary>
    /// Longitude in decimal degrees, from -180 through 180 relative to the prime meridian.
    /// </summary>
    [JsonPropertyName("longitude")]
    [JsonProperty("longitude")]
    public double Longitude { get; init; }

    /// <summary>
    /// Gets whether the coordinate is within valid geographic bounds.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool IsValid =>
        Latitude is >= -90 and <= 90 && Longitude is >= -180 and <= 180;

    /// <summary>
    /// Creates a coordinate from latitude and longitude in decimal degrees.
    /// </summary>
    /// <param name="latitude">Latitude in decimal degrees.</param>
    /// <param name="longitude">Longitude in decimal degrees.</param>
    [System.Text.Json.Serialization.JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public Coordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Returns the coordinate as an invariant latitude/longitude string.
    /// </summary>
    /// <returns>A string representation of the coordinate.</returns>
    public override string ToString()
    {
        return $"{Latitude.ToString(CultureInfo.InvariantCulture)}, {Longitude.ToString(CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Returns the coordinate formatted using the specified format and provider.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>A formatted string representation of the coordinate.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        formatProvider ??= CultureInfo.InvariantCulture;

        return $"{Latitude.ToString(format, formatProvider)}, {Longitude.ToString(format, formatProvider)}";
    }

    /// <summary>
    /// Attempts to format the coordinate into the provided character span.
    /// </summary>
    /// <param name="destination">The destination buffer.</param>
    /// <param name="charsWritten">The number of characters written.</param>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns><see langword="true"/> if formatting succeeded; otherwise <see langword="false"/>.</returns>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        string value = ToString(format.Length == 0 ? null : new string(format), provider);

        if (value.AsSpan().TryCopyTo(destination))
        {
            charsWritten = value.Length;
            return true;
        }

        charsWritten = 0;
        return false;
    }
}
