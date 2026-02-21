// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.FontManager
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Media.Fonts;
using Avalonia.Platform;
using Avalonia.Utilities;

public sealed class FontManager : IDisposable
{
	internal static Uri SystemFontsKey = new Uri("fonts:SystemFonts", UriKind.Absolute);

	private readonly ConcurrentDictionary<Uri, IFontCollection> _fontCollections = new ConcurrentDictionary<Uri, IFontCollection>();

	private readonly IReadOnlyList<FontFallback>? _fontFallbacks;

	private readonly IReadOnlyDictionary<string, FontFamily>? _fontFamilyMappings;

	public static FontManager Current
	{
		get
		{
			FontManager service = AvaloniaLocator.Current.GetService<FontManager>();
			if (service != null)
			{
				return service;
			}
			service = new FontManager(AvaloniaLocator.Current.GetRequiredService<IFontManagerImpl>());
			AvaloniaLocator.CurrentMutable.Bind<FontManager>().ToConstant(service);
			return service;
		}
	}

	public FontFamily DefaultFontFamily { get; }

	public IFontCollection SystemFonts => _fontCollections[SystemFontsKey];

	internal IFontManagerImpl PlatformImpl { get; }

	public FontManager(IFontManagerImpl P_0)
	{
		PlatformImpl = P_0;
		AddFontCollection(new SystemFontCollection(this));
		FontManagerOptions service = AvaloniaLocator.Current.GetService<FontManagerOptions>();
		_fontFallbacks = service?.FontFallbacks;
		_fontFamilyMappings = service?.FontFamilyMappings;
		string defaultFontFamilyName = GetDefaultFontFamilyName(service);
		DefaultFontFamily = new FontFamily(defaultFontFamilyName);
	}

	public bool TryGetGlyphTypeface(Typeface P_0, [NotNullWhen(true)] out IGlyphTypeface? P_1)
	{
		P_1 = null;
		FontFamily fontFamily = GetMappedFontFamily(P_0.FontFamily);
		if (P_0.FontFamily.Name == "$Default")
		{
			return TryGetGlyphTypeface(new Typeface(DefaultFontFamily, P_0.Style, P_0.Weight, P_0.Stretch), out P_1);
		}
		if (fontFamily.Key != null)
		{
			if (!(fontFamily.Key is CompositeFontFamilyKey compositeFontFamilyKey))
			{
				string primaryFamilyName = fontFamily.FamilyNames.PrimaryFamilyName;
				if (TryGetGlyphTypefaceByKeyAndName(P_0, fontFamily.Key, primaryFamilyName, out P_1))
				{
					return true;
				}
				return false;
			}
			for (int i = 0; i < compositeFontFamilyKey.Keys.Count; i++)
			{
				FontFamilyKey fontFamilyKey = compositeFontFamilyKey.Keys[i];
				string text = fontFamily.FamilyNames[i];
				if (_fontFamilyMappings != null && _fontFamilyMappings.TryGetValue(text, out FontFamily fontFamily2))
				{
					fontFamilyKey = ((!(fontFamily2.Key != null)) ? new FontFamilyKey(SystemFontsKey) : fontFamily2.Key);
					text = fontFamily2.FamilyNames.PrimaryFamilyName;
				}
				if (text == "$Default")
				{
					return TryGetGlyphTypeface(new Typeface(DefaultFontFamily, P_0.Style, P_0.Weight, P_0.Stretch), out P_1);
				}
				if (TryGetGlyphTypefaceByKeyAndName(P_0, fontFamilyKey, text, out P_1) && P_1.FamilyName.Contains(text))
				{
					return true;
				}
			}
		}
		else
		{
			string primaryFamilyName2 = fontFamily.FamilyNames.PrimaryFamilyName;
			if (SystemFonts.TryGetGlyphTypeface(primaryFamilyName2, P_0.Style, P_0.Weight, P_0.Stretch, out P_1))
			{
				return true;
			}
		}
		if (P_0.FontFamily == DefaultFontFamily)
		{
			return false;
		}
		return TryGetGlyphTypeface(new Typeface(DefaultFontFamily, P_0.Style, P_0.Weight, P_0.Stretch), out P_1);
		FontFamily GetMappedFontFamily(FontFamily fontFamily3)
		{
			if (_fontFamilyMappings == null || !_fontFamilyMappings.TryGetValue(fontFamily3.FamilyNames.PrimaryFamilyName, out FontFamily result))
			{
				return fontFamily3;
			}
			return result;
		}
	}

	private bool TryGetGlyphTypefaceByKeyAndName(Typeface P_0, FontFamilyKey P_1, string P_2, [NotNullWhen(true)] out IGlyphTypeface? P_3)
	{
		Uri uri = P_1.Source.EnsureAbsolute(P_1.BaseUri);
		if (TryGetFontCollection(uri, out IFontCollection fontCollection))
		{
			if (fontCollection.TryGetGlyphTypeface(P_2, P_0.Style, P_0.Weight, P_0.Stretch, out P_3))
			{
				return true;
			}
			Logger.TryGet(LogEventLevel.Debug, "FontManager")?.Log(this, $"Font family '{P_2}' could not be found. Present font families: [{string.Join(",", fontCollection)}]");
			return false;
		}
		P_3 = null;
		return false;
	}

	public void AddFontCollection(IFontCollection P_0)
	{
		Uri key = P_0.Key;
		if (!P_0.Key.IsFontCollection())
		{
			throw new ArgumentException("Font collection Key should follow the fonts: scheme.", "fontCollection");
		}
		_fontCollections.AddOrUpdate(key, P_0, delegate(Uri _, IFontCollection oldCollection)
		{
			oldCollection.Dispose();
			return P_0;
		});
		P_0.Initialize(PlatformImpl);
	}

	public bool TryMatchCharacter(int P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, FontFamily? P_4, CultureInfo? P_5, out Typeface P_6)
	{
		if (_fontFallbacks != null)
		{
			foreach (FontFallback fontFallback in _fontFallbacks)
			{
				if (fontFallback.UnicodeRange.IsInRange(P_0))
				{
					P_6 = new Typeface(fontFallback.FontFamily, P_1, P_2, P_3);
					if (TryGetGlyphTypeface(P_6, out IGlyphTypeface glyphTypeface) && glyphTypeface.TryGetGlyph((uint)P_0, out var _))
					{
						return true;
					}
				}
			}
		}
		if (P_4?.Key != null)
		{
			if (P_4.Key is CompositeFontFamilyKey compositeFontFamilyKey)
			{
				for (int i = 0; i < compositeFontFamilyKey.Keys.Count; i++)
				{
					FontFamilyKey fontFamilyKey = compositeFontFamilyKey.Keys[i];
					string text = P_4.FamilyNames[i];
					Uri uri = fontFamilyKey.Source.EnsureAbsolute(fontFamilyKey.BaseUri);
					if (text == "$Default")
					{
						text = DefaultFontFamily.Name;
					}
					if (TryGetFontCollection(uri, out IFontCollection fontCollection) && fontCollection.TryGetGlyphTypeface(text, P_1, P_2, P_3, out IGlyphTypeface _) && fontCollection.TryMatchCharacter(P_0, P_1, P_2, P_3, text, P_5, out P_6))
					{
						return true;
					}
				}
			}
			Uri uri2 = P_4.Key.Source.EnsureAbsolute(P_4.Key.BaseUri);
			if (uri2.IsFontCollection() && TryGetFontCollection(uri2, out IFontCollection fontCollection2) && fontCollection2.TryMatchCharacter(P_0, P_1, P_2, P_3, P_4.Name, P_5, out P_6))
			{
				return true;
			}
		}
		return SystemFonts.TryMatchCharacter(P_0, P_1, P_2, P_3, P_4?.Name, P_5, out P_6);
	}

	private bool TryGetFontCollection(Uri P_0, [NotNullWhen(true)] out IFontCollection? P_1)
	{
		if (P_0.Scheme == "systemfont")
		{
			P_0 = SystemFontsKey;
		}
		if (!_fontCollections.TryGetValue(P_0, out P_1) && (P_0.IsAbsoluteResm() || P_0.IsAvares()))
		{
			EmbeddedFontCollection embeddedFontCollection = new EmbeddedFontCollection(P_0, P_0);
			embeddedFontCollection.Initialize(PlatformImpl);
			if (embeddedFontCollection.Count > 0 && _fontCollections.TryAdd(P_0, embeddedFontCollection))
			{
				P_1 = embeddedFontCollection;
			}
		}
		return P_1 != null;
	}

	private string GetDefaultFontFamilyName(FontManagerOptions? P_0)
	{
		string text = P_0?.DefaultFamilyName ?? PlatformImpl.GetDefaultFontFamilyName();
		if (string.IsNullOrEmpty(text) && SystemFonts.Count > 0)
		{
			text = SystemFonts[0].Name;
		}
		if (string.IsNullOrEmpty(text))
		{
			throw new InvalidOperationException("Default font family name can't be null or empty.");
		}
		if (text == "$Default")
		{
			throw new InvalidOperationException("'$Default' is a placeholder and cannot be used as the default font family name. Provide a concrete font family name via FontManagerOptions or the platform implementation.");
		}
		return text;
	}

	void IDisposable.Dispose()
	{
		foreach (KeyValuePair<Uri, IFontCollection> fontCollection in _fontCollections)
		{
			fontCollection.Value.Dispose();
		}
		_fontCollections.Clear();
		(PlatformImpl as IDisposable)?.Dispose();
	}
}

