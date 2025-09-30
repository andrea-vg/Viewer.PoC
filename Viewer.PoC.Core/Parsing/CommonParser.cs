using System;
using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Extensions;
using Viewer.PoC.Core.Logging;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Parsing
{
    internal class CommonParser
    {
        internal static Coordinates ParseCoordinates(string input, ILogging logger = null)
        {
            logger?.Info($"Input: '{input}'");
            if (string.IsNullOrWhiteSpace(input))
            {
                logger?.Warn("Input is null or whitespace, returning (0,0)");
                return new Coordinates(0, 0);
            }
            var parts = input.SplitAndTrim(';');

            if (parts.Length == 2)
            {
                try
                {
                    var partX = parts[0].Trim().NormalizeDecimalSeparator();
                    var partyY = parts[1].Trim().NormalizeDecimalSeparator();
                    if (double.TryParse(partX, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var x)
                     && double.TryParse(partyY, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var y))
                    {
                        logger?.Info($"Parsed ({x}, {y})");
                        return new Coordinates(x, y);
                    }
                    else
                    {
                        throw new FormatException($"Invalid coordinate values: '{partX}', '{partyY}'");
                    }
                }
                catch (Exception ex)
                {
                    logger?.Error($"Coordinates were failed to parse '{input}'", ex);
                    throw new ShapeParseException($"Invalid coordinate string: '{input}'", ex);
                }
            }
            logger?.Warn($"There should not be less or more than 2 parts in '{input}': returning (0,0)");
            return new Coordinates(0, 0);
        }

        internal static Color ParseColor(string input, ILogging logger = null)
        {
            logger?.Info($"ParseColor input: '{input}'");
            if (string.IsNullOrWhiteSpace(input))
            {
                logger?.Warn("ParseColor: input is null or whitespace, returning default color");
                return new Color(255, 0, 0, 0);
            }
            var parts = input.SplitAndTrim(';');
            byte a = 255;
            byte r = 0;
            byte g = 0;
            byte b = 0;
            if (parts.Length >= 4)
            {
                a = parts[0].ToByte();
                r = parts[1].ToByte();
                g = parts[2].ToByte();
                b = parts[3].ToByte();
                logger?.Info($"ParseColor: parsed ARGB=({a},{r},{g},{b})");
            }
            else
            {
                logger?.Warn($"ParseColor: not enough parts in '{input}', using default color ARGB=({a},{r},{g},{b})");
            }
            return new Color(a, r, g, b);
        }
    }
}