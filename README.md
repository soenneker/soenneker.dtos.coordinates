[![](https://img.shields.io/nuget/v/soenneker.dtos.coordinates.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.coordinates/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.coordinates/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.coordinates/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dtos.coordinates.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.coordinates/)

# Soenneker.Dtos.Coordinates

Identifies a geographic point using WGS84 latitude and longitude expressed in decimal degrees.

## Install

```bash
dotnet add package Soenneker.Dtos.Coordinates
```

## What you get

- `Coordinate` — Identifies a geographic point using WGS84 latitude and longitude expressed in decimal degrees.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `Coordinate.Latitude` | Latitude in decimal degrees, from -90 at the South Pole through 90 at the North Pole. | Latitude in decimal degrees, from -90 at the South Pole through 90 at the North Pole. |
| `Coordinate.Longitude` | Longitude in decimal degrees, from -180 through 180 relative to the prime meridian. | Longitude in decimal degrees, from -180 through 180 relative to the prime meridian. |
| `Coordinate.IsValid` | Gets whether the coordinate is within valid geographic bounds. | Gets whether the coordinate is within valid geographic bounds. |
| `Coordinate.ToString()` | Returns the coordinate as an invariant latitude/longitude string. | A string representation of the coordinate. |
| `Coordinate.ToString(format, formatProvider)` | Returns the coordinate formatted using the specified format and provider. | A formatted string representation of the coordinate. |
| `Coordinate.TryFormat(destination, charsWritten, format, provider)` | Attempts to format the coordinate into the provided character span. | `true` if formatting succeeded; otherwise `false`. |
