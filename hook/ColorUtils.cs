using System.Text.RegularExpressions;

namespace Uprooted;

/// <summary>
/// Static color manipulation utilities for the custom theme engine.
/// All methods work with "#RRGGBB" hex strings (6-digit, with hash prefix).
/// </summary>
internal static class ColorUtils
{
    private static readonly Regex HexPattern = new(@"^#[0-9A-Fa-f]{6}$");

    public static bool IsValidHex(string? hex)
        => hex != null && HexPattern.IsMatch(hex);

    public static (byte R, byte G, byte B) ParseHex(string hex)
    {
        var h = hex.TrimStart('#');
        if (h.Length == 8) h = h[2..]; // strip alpha prefix
        return (
            Convert.ToByte(h[0..2], 16),
            Convert.ToByte(h[2..4], 16),
            Convert.ToByte(h[4..6], 16)
        );
    }

    public static string ToHex(byte r, byte g, byte b)
        => $"#{r:X2}{g:X2}{b:X2}";

    /// <summary>
    /// Darken a color by a percentage (0-100). Multiplies each channel by (1 - pct/100).
    /// </summary>
    public static string Darken(string hex, double percent)
    {
        var (r, g, b) = ParseHex(hex);
        double factor = 1.0 - Math.Clamp(percent, 0, 100) / 100.0;
        return ToHex(
            (byte)Math.Round(r * factor),
            (byte)Math.Round(g * factor),
            (byte)Math.Round(b * factor)
        );
    }

    /// <summary>
    /// Lighten a color by a percentage (0-100). Blends toward white:
    /// channel + (255 - channel) * pct/100
    /// </summary>
    public static string Lighten(string hex, double percent)
    {
        var (r, g, b) = ParseHex(hex);
        double factor = Math.Clamp(percent, 0, 100) / 100.0;
        return ToHex(
            (byte)Math.Round(r + (255 - r) * factor),
            (byte)Math.Round(g + (255 - g) * factor),
            (byte)Math.Round(b + (255 - b) * factor)
        );
    }

    /// <summary>
    /// Prepend an alpha byte to produce "#AARRGGBB" format (Avalonia convention).
    /// Alpha is 0-255 integer.
    /// </summary>
    public static string WithAlpha(string hex, int alpha)
    {
        var (r, g, b) = ParseHex(hex);
        byte a = (byte)Math.Clamp(alpha, 0, 255);
        return $"#{a:X2}{r:X2}{g:X2}{b:X2}";
    }

    /// <summary>
    /// Relative luminance per WCAG 2.0 formula (0.0 = black, 1.0 = white).
    /// </summary>
    public static double Luminance(string hex)
    {
        var (r, g, b) = ParseHex(hex);
        double rs = Linearize(r / 255.0);
        double gs = Linearize(g / 255.0);
        double bs = Linearize(b / 255.0);
        return 0.2126 * rs + 0.7152 * gs + 0.0722 * bs;
    }

    private static double Linearize(double c)
        => c <= 0.03928 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);

    /// <summary>
    /// Returns near-white for dark backgrounds, near-black for light backgrounds.
    /// </summary>
    public static string DeriveTextColor(string bgHex)
        => Luminance(bgHex) > 0.3 ? "#1A1A1A" : "#F0F0F0";

    /// <summary>
    /// Mix two hex colors by a ratio (0.0 = all colorA, 1.0 = all colorB).
    /// </summary>
    public static string Mix(string hexA, string hexB, double ratio)
    {
        var (ra, ga, ba) = ParseHex(hexA);
        var (rb, gb, bb) = ParseHex(hexB);
        double t = Math.Clamp(ratio, 0, 1);
        return ToHex(
            (byte)Math.Round(ra + (rb - ra) * t),
            (byte)Math.Round(ga + (gb - ga) * t),
            (byte)Math.Round(ba + (bb - ba) * t)
        );
    }

    /// <summary>
    /// Produce a hex string with alpha as a fraction (0.0-1.0) -> "#AARRGGBB".
    /// </summary>
    public static string WithAlphaFraction(string hex, double alpha)
        => WithAlpha(hex, (int)Math.Round(Math.Clamp(alpha, 0, 1) * 255));
}
