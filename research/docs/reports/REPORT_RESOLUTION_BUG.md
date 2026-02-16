Image Resolution Bug

In uriToImageUrl there's a typo that's been there since v0.0.40. The second ? should be &

Broken:  "asset?query=" + encodeURI(uri) + "?resolution=" + encodeURI(resolution)
Fixed:   "asset?query=" + encodeURI(uri) + "&resolution=" + encodeURI(resolution)

The double ? means the server never receives the resolution as a separate parameter. Every image loads at default resolution. Same bug in all 7 sub-app bundles.
