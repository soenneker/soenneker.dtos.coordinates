[![](https://img.shields.io/nuget/v/soenneker.dtos.coordinates.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.coordinates/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.coordinates/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.coordinates/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dtos.coordinates.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.coordinates/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.coordinates/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.coordinates/actions/workflows/codeql.yml)

# Soenneker.Dtos.Coordinates

Defines an immutable WGS84 latitude/longitude value with JSON mapping, bounds inspection, and invariant formatting.

## Installation

```bash
dotnet add package Soenneker.Dtos.Coordinates
```

## Usage

```csharp
using Soenneker.Dtos.Coordinates;

var chicago = new Coordinate(
    latitude: 41.8781,
    longitude: -87.6298);

if (!chicago.IsValid)
{
    throw new InvalidOperationException("Coordinate is outside WGS84 bounds.");
}

string text = chicago.ToString(); // 41.8781, -87.6298
```

Constructor and formatted output order is latitude first, longitude second. GeoJSON coordinate arrays use longitude first, so swap deliberately when adapting to or from GeoJSON.

## Validation

`IsValid` is true when latitude is between -90 and 90 and longitude is between -180 and 180, inclusive. `NaN` and infinity are invalid. Construction and deserialization do not reject invalid values; inspect `IsValid` at the boundary where invalid coordinates must be refused.

`default(Coordinate)` represents `0, 0` and is valid. Do not use the default value as an “unset” sentinel; use `Coordinate?` when absence must be represented.

## Serialization and formatting

System.Text.Json and Newtonsoft.Json serialize the value as:

```json
{
  "latitude": 41.8781,
  "longitude": -87.6298
}
```

`IsValid` is ignored by both serializers.

`ToString()` always uses invariant culture and produces `latitude, longitude`. The `ISpanFormattable` overload applies the same numeric format and provider to both values:

```csharp
string rounded = chicago.ToString("F2", CultureInfo.InvariantCulture);
// 41.88, -87.63

Span<char> buffer = stackalloc char[64];
bool formatted = chicago.TryFormat(buffer, out int written, "F4", CultureInfo.InvariantCulture);
```

When the destination is too small, `TryFormat` returns false and sets `written` to zero.
