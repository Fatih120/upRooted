using Xunit;

namespace Uprooted.Tests;

/// <summary>
/// Tests for ClearUrlsEngine.CleanUrl — pure static URL cleaning logic.
/// No Avalonia, no filesystem, no network required.
/// </summary>
public class ClearUrlsEngineTests
{
    // ── All 33 tracking params, each stripped individually ──

    [Theory]
    [InlineData("utm_source")]
    [InlineData("utm_medium")]
    [InlineData("utm_campaign")]
    [InlineData("utm_term")]
    [InlineData("utm_content")]
    [InlineData("utm_id")]
    [InlineData("utm_cid")]
    [InlineData("fbclid")]
    [InlineData("gclid")]
    [InlineData("gclsrc")]
    [InlineData("dclid")]
    [InlineData("msclkid")]
    [InlineData("igshid")]
    [InlineData("twclid")]
    [InlineData("si")]
    [InlineData("feature")]
    [InlineData("mc_cid")]
    [InlineData("mc_eid")]
    [InlineData("_ga")]
    [InlineData("_gl")]
    [InlineData("_gid")]
    [InlineData("yclid")]
    [InlineData("_hsenc")]
    [InlineData("_hsmi")]
    [InlineData("__hstc")]
    [InlineData("__hsfp")]
    [InlineData("hsCtaTracking")]
    [InlineData("wickedid")]
    [InlineData("rb_clickid")]
    [InlineData("soc_src")]
    [InlineData("soc_trk")]
    [InlineData("vero_id")]
    [InlineData("_openstat")]
    public void SingleTrackingParam_IsStripped(string param)
    {
        var url = $"https://example.com/page?{param}=abc123";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/page", result);
    }

    // ── Multiple tracking params stripped at once ──

    [Fact]
    public void MultipleTrackingParams_AllStripped()
    {
        var url = "https://example.com/?utm_source=google&fbclid=xyz&gclid=123";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/", result);
    }

    [Fact]
    public void ThreeUtmParams_AllStripped()
    {
        var url = "https://example.com/article?utm_source=newsletter&utm_medium=email&utm_campaign=spring2026";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/article", result);
    }

    // ── Non-tracking params preserved ──

    [Fact]
    public void NonTrackingParam_Preserved()
    {
        var url = "https://example.com/search?q=hello+world";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    [Fact]
    public void MultipleNonTrackingParams_AllPreserved()
    {
        var url = "https://example.com/search?q=test&page=2&sort=date";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    // ── Mix of tracking + non-tracking ──

    [Fact]
    public void MixedParams_OnlyTrackingStripped()
    {
        var url = "https://example.com/item?id=42&utm_source=google&color=blue";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/item?id=42&color=blue", result);
    }

    [Fact]
    public void TrackingParamFirst_NonTrackingPreserved()
    {
        var url = "https://example.com/?fbclid=abc&ref=nav";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/?ref=nav", result);
    }

    [Fact]
    public void TrackingParamLast_NonTrackingPreserved()
    {
        var url = "https://example.com/?ref=nav&fbclid=abc";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/?ref=nav", result);
    }

    // ── Fragment (#hash) handling ──

    [Fact]
    public void Fragment_PreservedWhenParamsStripped()
    {
        var url = "https://example.com/page?utm_source=google#section";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/page#section", result);
    }

    [Fact]
    public void Fragment_PreservedWhenAllParamsStripped()
    {
        var url = "https://example.com/page?utm_source=google&fbclid=x#top";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/page#top", result);
    }

    [Fact]
    public void Fragment_PreservedWhenNoQueryString()
    {
        var url = "https://example.com/page#section";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    [Fact]
    public void Fragment_PreservedWhenNoTrackingParamsToStrip()
    {
        var url = "https://example.com/page?id=5#anchor";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    // ── No changes needed ──

    [Fact]
    public void NoQueryString_ReturnedUnchanged()
    {
        var url = "https://example.com/page";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    [Fact]
    public void QueryWithNoTrackingParams_ReturnedUnchanged()
    {
        var url = "https://example.com/?q=cats&page=1";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    [Fact]
    public void EmptyQueryString_ReturnedUnchanged()
    {
        var url = "https://example.com/page?";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    // ── Idempotency ──

    [Fact]
    public void Idempotency_CleanOfCleanEqualsClean()
    {
        var url = "https://example.com/page?id=42&utm_source=google&color=blue";
        var once = ClearUrlsEngine.CleanUrl(url);
        var twice = ClearUrlsEngine.CleanUrl(once);
        Assert.Equal(once, twice);
    }

    [Fact]
    public void Idempotency_AlreadyCleanUrl()
    {
        var url = "https://example.com/page?id=42";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    // ── Edge cases ──

    [Fact]
    public void EmptyParamValue_TrackingParamStillStripped()
    {
        var url = "https://example.com/?utm_source=&id=1";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/?id=1", result);
    }

    [Fact]
    public void ValuelessTrackingParam_Stripped()
    {
        // Param with no '=' — key is the entire part
        var url = "https://example.com/?fbclid&id=1";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/?id=1", result);
    }

    [Fact]
    public void ParamStartingWithTrackingName_NotStripped()
    {
        // utm_source2 is NOT a tracking param — prefix match safety
        var url = "https://example.com/?utm_source2=foo";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal(url, result);
    }

    [Fact]
    public void CaseInsensitive_UpperCaseParamStripped()
    {
        // TrackingParams uses StringComparer.OrdinalIgnoreCase
        var url = "https://example.com/?UTM_SOURCE=google";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/", result);
    }

    [Fact]
    public void CaseInsensitive_MixedCaseParamStripped()
    {
        var url = "https://example.com/?FbClId=abc123";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/", result);
    }

    [Fact]
    public void ParamValueContainsEquals_FullValuePreserved()
    {
        // Non-tracking param whose value contains '='
        var url = "https://example.com/?data=a=b&utm_source=x";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/?data=a=b", result);
    }

    [Fact]
    public void LongUrlManyParams_OnlyTrackingStripped()
    {
        var url = "https://shop.example.com/products?id=123&category=shoes&size=42&color=red&utm_source=email&utm_medium=cta&utm_campaign=sale2026&fbclid=abc&sort=price&page=1";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://shop.example.com/products?id=123&category=shoes&size=42&color=red&sort=price&page=1", result);
    }

    [Fact]
    public void FragmentBeforeQuery_FragmentNotMistreatedAsQuery()
    {
        // '#' before '?' — the '?' is inside the fragment, not a real query string
        var url = "https://example.com/page#section?utm_source=x";
        var result = ClearUrlsEngine.CleanUrl(url);
        // The whole #section?utm_source=x is the fragment — no query to strip
        Assert.Equal(url, result);
    }

    [Fact]
    public void MixedParams_WithFragment_TrackingStrippedFragmentKept()
    {
        var url = "https://example.com/doc?chapter=3&utm_medium=social&_ga=2.123#intro";
        var result = ClearUrlsEngine.CleanUrl(url);
        Assert.Equal("https://example.com/doc?chapter=3#intro", result);
    }
}
