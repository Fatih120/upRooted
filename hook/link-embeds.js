// Uprooted Link Embeds
// Injected into DotNetBrowser main frame via ExecuteJavaScript.
// Discord-style link previews: MutationObserver watches for <a> elements,
// fetches OpenGraph metadata, renders embed cards inline.
(function() {
    'use strict';

    // Self-guard: don't inject twice
    if (window.__UPROOTED_LINK_EMBEDS_ACTIVE__) return;
    window.__UPROOTED_LINK_EMBEDS_ACTIVE__ = true;

    var LOG = '[Uprooted:link-embeds]';
    var DEBUG_URL = 'http://localhost:9876/log';
    var FETCH_TIMEOUT = 8000;
    var MAX_HTML_BYTES = 50000;

    // Stash raw fetch/XHR before any plugin wraps them
    var _rawXHR = window.XMLHttpRequest;

    function dbg(msg) {
        var line = '[' + new Date().toLocaleTimeString() + '] ' + msg;
        console.log(LOG + ' ' + msg);
        try {
            var xhr = new _rawXHR();
            xhr.open('POST', DEBUG_URL, true);
            xhr.setRequestHeader('Content-Type', 'text/plain');
            xhr.send(LOG + ' ' + line);
        } catch(e) {}
    }

    // ── Settings ──────────────────────────────────────────────
    function getConfig() {
        var s = window.__UPROOTED_SETTINGS__;
        var config = s && s.plugins && s.plugins['link-embeds'] && s.plugins['link-embeds'].config;
        return {
            youtube: config && typeof config.youtube === 'boolean' ? config.youtube : true,
            websites: config && typeof config.websites === 'boolean' ? config.websites : true,
            maxEmbedsPerMessage: config && typeof config.maxEmbedsPerMessage === 'number' ? config.maxEmbedsPerMessage : 3
        };
    }

    // ── Inline CSS ────────────────────────────────────────────
    var CSS = [
        '.uprooted-embed {',
        '  display: flex;',
        '  max-width: 520px;',
        '  margin-top: 4px;',
        '  margin-bottom: 4px;',
        '  padding: 12px 16px;',
        '  border-left: 4px solid var(--rootsdk-brand-primary, #3B6AF8);',
        '  border-radius: 0 4px 4px 0;',
        '  background: var(--rootsdk-background-secondary, #1A1E24);',
        '  font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;',
        '  overflow: hidden;',
        '}',
        '.uprooted-embed--youtube { flex-direction: column; }',
        '.uprooted-embed-body {',
        '  flex: 1; min-width: 0; display: flex; flex-direction: column; gap: 4px;',
        '}',
        '.uprooted-embed-provider {',
        '  font-size: 12px; font-weight: 500; color: var(--rootsdk-muted, #72767D);',
        '}',
        '.uprooted-embed-title {',
        '  font-size: 14px; font-weight: 600;',
        '  color: var(--rootsdk-link, var(--rootsdk-brand-primary, #3B6AF8));',
        '  text-decoration: none; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;',
        '}',
        '.uprooted-embed-title:hover { text-decoration: underline; }',
        '.uprooted-embed-description {',
        '  font-size: 13px; line-height: 1.4; color: var(--rootsdk-text-secondary, #B5B5B5);',
        '  display: -webkit-box; -webkit-line-clamp: 3; -webkit-box-orient: vertical; overflow: hidden;',
        '}',
        '.uprooted-embed-thumbnail { flex-shrink: 0; margin-left: 16px; align-self: flex-start; }',
        '.uprooted-embed-thumbnail img {',
        '  width: 80px; height: 80px; border-radius: 4px; object-fit: cover;',
        '}',
        '.uprooted-embed-video {',
        '  position: relative; margin-top: 8px; border-radius: 4px; overflow: hidden;',
        '  cursor: pointer; aspect-ratio: 16 / 9; max-width: 400px; background: #000;',
        '}',
        '.uprooted-embed-video-thumb { width: 100%; height: 100%; object-fit: cover; display: block; }',
        '.uprooted-embed-yt-play {',
        '  position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);',
        '  opacity: 0.85; transition: opacity 0.15s; pointer-events: none;',
        '}',
        '.uprooted-embed-video:hover .uprooted-embed-yt-play { opacity: 1; }',
        '.uprooted-embed-yt-iframe { width: 100%; height: 100%; border: none; }'
    ].join('\n');

    function injectCSS() {
        if (document.getElementById('uprooted-link-embeds-css')) return;
        var style = document.createElement('style');
        style.id = 'uprooted-link-embeds-css';
        style.textContent = CSS;
        (document.head || document.documentElement).appendChild(style);
        dbg('CSS injected');
    }

    // ── YouTube parsing ───────────────────────────────────────
    function parseYouTubeId(url) {
        try {
            var u = new URL(url);
            var host = u.hostname.replace('www.', '');
            if (host === 'youtube.com' || host === 'm.youtube.com') {
                if (u.pathname === '/watch') return u.searchParams.get('v');
                var m = u.pathname.match(/^\/(embed|shorts)\/([^/?&]+)/);
                if (m) return m[2];
            }
            if (host === 'youtu.be') {
                var id = u.pathname.slice(1).split(/[/?&]/)[0];
                return id || null;
            }
        } catch(e) {}
        return null;
    }

    // ── OpenGraph parsing ─────────────────────────────────────
    function parseOpenGraph(html) {
        var result = {};
        // Match <meta property="og:..." content="..."> (both attribute orders)
        var re1 = /<meta\s+(?:[^>]*?\s)?(?:property|name)\s*=\s*["']([^"']+)["'][^>]*?\scontent\s*=\s*["']([^"']*)["'][^>]*?\/?>/gi;
        var m;
        while ((m = re1.exec(html)) !== null) {
            result[m[1].toLowerCase()] = m[2];
        }
        var re2 = /<meta\s+(?:[^>]*?\s)?content\s*=\s*["']([^"']*)["'][^>]*?\s(?:property|name)\s*=\s*["']([^"']+)["'][^>]*?\/?>/gi;
        while ((m = re2.exec(html)) !== null) {
            var k = m[2].toLowerCase();
            if (!result[k]) result[k] = m[1];
        }
        return {
            title: result['og:title'],
            description: result['og:description'],
            image: result['og:image'],
            siteName: result['og:site_name'],
            themeColor: result['theme-color']
        };
    }

    // ── Metadata fetching ─────────────────────────────────────
    var metadataCache = {};

    function fetchMetadata(url, callback) {
        if (metadataCache.hasOwnProperty(url)) {
            dbg('Cache hit: ' + url.slice(0, 60));
            return callback(metadataCache[url]);
        }

        var videoId = parseYouTubeId(url);
        if (videoId) {
            dbg('YouTube: ' + url.slice(0, 60) + ' (id: ' + videoId + ')');
            fetchYouTubeMetadata(url, videoId, function(data) {
                metadataCache[url] = data;
                callback(data);
            });
            return;
        }

        dbg('Generic: ' + url.slice(0, 60));
        fetchGenericMetadata(url, function(data) {
            metadataCache[url] = data;
            callback(data);
        });
    }

    function fetchYouTubeMetadata(url, videoId, callback) {
        var data = {
            url: url,
            type: 'youtube',
            provider: 'YouTube',
            videoId: videoId,
            image: 'https://img.youtube.com/vi/' + videoId + '/hqdefault.jpg',
            color: '#FF0000'
        };

        // Try oEmbed for the title
        try {
            var controller = new AbortController();
            var timer = setTimeout(function() { controller.abort(); }, FETCH_TIMEOUT);

            fetch('https://www.youtube.com/oembed?url=' + encodeURIComponent(url) + '&format=json', {
                signal: controller.signal
            }).then(function(resp) {
                clearTimeout(timer);
                if (resp.ok) return resp.json();
                return null;
            }).then(function(json) {
                if (json) {
                    data.title = json.title;
                    if (json.author_name) data.description = json.author_name;
                }
                callback(data);
            }).catch(function() {
                clearTimeout(timer);
                callback(data); // thumbnail still works without title
            });
        } catch(e) {
            callback(data);
        }
    }

    function fetchGenericMetadata(url, callback) {
        var controller;
        var timer;
        try { controller = new AbortController(); } catch(e) { controller = null; }

        var opts = { headers: { 'Accept': 'text/html' } };
        if (controller) {
            opts.signal = controller.signal;
            timer = setTimeout(function() { controller.abort(); }, FETCH_TIMEOUT);
        }

        fetch(url, opts).then(function(resp) {
            if (!resp.ok) {
                dbg('Non-OK ' + resp.status + ': ' + url.slice(0, 60));
                return null;
            }
            var ct = resp.headers.get('content-type') || '';
            if (ct.indexOf('text/html') === -1) {
                dbg('Non-HTML: ' + url.slice(0, 60));
                return null;
            }
            // Read only first 50KB
            var reader = resp.body && resp.body.getReader ? resp.body.getReader() : null;
            if (!reader) return resp.text().then(function(t) { return t.slice(0, MAX_HTML_BYTES); });

            var decoder = new TextDecoder();
            var html = '';
            function pump() {
                return reader.read().then(function(result) {
                    if (result.done || html.length >= MAX_HTML_BYTES) {
                        reader.cancel().catch(function(){});
                        return html;
                    }
                    html += decoder.decode(result.value, { stream: true });
                    return pump();
                });
            }
            return pump();
        }).then(function(html) {
            if (timer) clearTimeout(timer);
            if (!html) return callback(null);

            var og = parseOpenGraph(html);
            if (!og.title) {
                var tm = html.match(/<title[^>]*>([^<]+)<\/title>/i);
                if (tm) og.title = tm[1].trim();
            }
            if (!og.title) return callback(null);

            // Resolve relative image URLs
            var image = og.image;
            if (image && image.indexOf('http') !== 0) {
                try { image = new URL(image, url).href; } catch(e) { image = undefined; }
            }

            // Extract hostname for provider
            var provider = og.siteName;
            if (!provider) {
                try { provider = new URL(url).hostname.replace('www.', ''); } catch(e) {}
            }

            callback({
                url: url,
                type: 'generic',
                provider: provider,
                title: og.title,
                description: og.description,
                image: image,
                color: og.themeColor
            });
        }).catch(function(err) {
            if (timer) clearTimeout(timer);
            dbg('Fetch error: ' + url.slice(0, 60) + ' ' + (err.message || err));
            callback(null);
        });
    }

    // ── Embed card DOM ────────────────────────────────────────
    function truncate(text, max) {
        if (text.length <= max) return text;
        return text.slice(0, max).trimEnd() + '\u2026';
    }

    function createEmbedCard(data) {
        if (data.type === 'youtube' && data.videoId) return createYouTubeEmbed(data);

        var card = document.createElement('div');
        card.className = 'uprooted-embed';
        if (data.color) card.style.borderLeftColor = data.color;

        var body = document.createElement('div');
        body.className = 'uprooted-embed-body';

        if (data.provider) {
            var prov = document.createElement('div');
            prov.className = 'uprooted-embed-provider';
            prov.textContent = data.provider;
            if (data.color) prov.style.color = data.color;
            body.appendChild(prov);
        }

        if (data.title) {
            var title = document.createElement('a');
            title.className = 'uprooted-embed-title';
            title.href = data.url;
            title.target = '_blank';
            title.rel = 'noopener noreferrer';
            title.textContent = data.title;
            body.appendChild(title);
        }

        if (data.description) {
            var desc = document.createElement('div');
            desc.className = 'uprooted-embed-description';
            desc.textContent = truncate(data.description, 250);
            body.appendChild(desc);
        }

        card.appendChild(body);

        if (data.image) {
            var thumbWrap = document.createElement('div');
            thumbWrap.className = 'uprooted-embed-thumbnail';
            var img = document.createElement('img');
            img.src = data.image;
            img.alt = data.title || '';
            img.loading = 'lazy';
            img.onerror = function() { thumbWrap.remove(); };
            thumbWrap.appendChild(img);
            card.appendChild(thumbWrap);
        }

        return card;
    }

    function createYouTubeEmbed(data) {
        var card = document.createElement('div');
        card.className = 'uprooted-embed uprooted-embed--youtube';
        if (data.color) card.style.borderLeftColor = data.color;

        var body = document.createElement('div');
        body.className = 'uprooted-embed-body';

        var prov = document.createElement('div');
        prov.className = 'uprooted-embed-provider';
        prov.textContent = 'YouTube';
        prov.style.color = '#FF0000';
        body.appendChild(prov);

        if (data.title) {
            var title = document.createElement('a');
            title.className = 'uprooted-embed-title';
            title.href = data.url;
            title.target = '_blank';
            title.rel = 'noopener noreferrer';
            title.textContent = data.title;
            body.appendChild(title);
        }

        if (data.description) {
            var desc = document.createElement('div');
            desc.className = 'uprooted-embed-description';
            desc.textContent = data.description;
            body.appendChild(desc);
        }

        card.appendChild(body);

        var videoWrap = document.createElement('div');
        videoWrap.className = 'uprooted-embed-video';

        var img = document.createElement('img');
        img.className = 'uprooted-embed-video-thumb';
        img.src = data.image || ('https://img.youtube.com/vi/' + data.videoId + '/hqdefault.jpg');
        img.alt = data.title || 'YouTube video';
        img.loading = 'lazy';

        var playBtn = document.createElement('div');
        playBtn.className = 'uprooted-embed-yt-play';
        playBtn.innerHTML =
            '<svg viewBox="0 0 68 48" width="68" height="48">' +
            '<path d="M66.52 7.74c-.78-2.93-2.49-5.41-5.42-6.19C55.79.13 34 0 34 0S12.21.13 6.9 1.55C3.97 2.33 2.27 4.81 1.48 7.74.06 13.05 0 24 0 24s.06 10.95 1.48 16.26c.78 2.93 2.49 5.41 5.42 6.19C12.21 47.87 34 48 34 48s21.79-.13 27.1-1.55c2.93-.78 4.64-3.26 5.42-6.19C67.94 34.95 68 24 68 24s-.06-10.95-1.48-16.26z" fill="#FF0000"/>' +
            '<path d="M45 24L27 14v20" fill="#fff"/>' +
            '</svg>';

        videoWrap.appendChild(img);
        videoWrap.appendChild(playBtn);

        videoWrap.addEventListener('click', function() {
            var iframe = document.createElement('iframe');
            iframe.className = 'uprooted-embed-yt-iframe';
            iframe.src = 'https://www.youtube.com/embed/' + data.videoId + '?autoplay=1';
            iframe.allow = 'autoplay; encrypted-media';
            iframe.allowFullscreen = true;
            iframe.setAttribute('frameborder', '0');
            videoWrap.innerHTML = '';
            videoWrap.appendChild(iframe);
        });

        card.appendChild(videoWrap);
        return card;
    }

    // ── Link processing ───────────────────────────────────────
    var LINK_RE = /^https?:\/\//;
    var processedLinks = new WeakSet();

    function countEmbedsInContext(anchor) {
        var container = anchor.parentElement;
        for (var i = 0; i < 5 && container; i++) container = container.parentElement;
        if (!container) container = anchor.parentElement;
        return container ? container.querySelectorAll('.uprooted-embed').length : 0;
    }

    function findInsertionPoint(anchor) {
        var block = anchor;
        while (block && block !== document.body) {
            var display = getComputedStyle(block).display;
            if (display === 'block' || display === 'flex' || display === 'grid') {
                return { parent: block.parentNode, ref: block.nextSibling };
            }
            block = block.parentElement;
        }
        return { parent: anchor.parentNode, ref: anchor.nextSibling };
    }

    function processLink(anchor) {
        if (processedLinks.has(anchor)) return;
        processedLinks.add(anchor);

        var href = anchor.href;
        if (!LINK_RE.test(href)) return;

        // Skip Uprooted UI links
        if (anchor.closest('[id^="uprooted-"], [data-uprooted]')) return;

        var config = getConfig();
        var isYouTube = /(?:youtube\.com|youtu\.be)/.test(href);

        if (isYouTube && !config.youtube) return;
        if (!isYouTube && !config.websites) return;
        if (countEmbedsInContext(anchor) >= config.maxEmbedsPerMessage) return;

        dbg('Processing: ' + href.slice(0, 80));

        fetchMetadata(href, function(data) {
            if (!data) {
                dbg('No metadata: ' + href.slice(0, 60));
                return;
            }
            if (!anchor.isConnected) {
                dbg('Anchor gone: ' + href.slice(0, 60));
                return;
            }
            if (countEmbedsInContext(anchor) >= config.maxEmbedsPerMessage) return;

            dbg('Got: "' + (data.title || '') + '" [' + data.type + ']');
            var card = createEmbedCard(data);
            var point = findInsertionPoint(anchor);

            try {
                point.parent.insertBefore(card, point.ref);
                dbg('INSERTED: ' + href.slice(0, 60));
            } catch(e) {
                try {
                    anchor.parentNode.insertBefore(card, anchor.nextSibling);
                    dbg('INSERTED (fallback): ' + href.slice(0, 60));
                } catch(e2) {
                    dbg('Insert failed: ' + href.slice(0, 60));
                }
            }
        });
    }

    function scanForLinks(root) {
        if (!(root instanceof HTMLElement)) return;
        var anchors = root.querySelectorAll('a[href]');
        for (var i = 0; i < anchors.length; i++) {
            var a = anchors[i];
            if (!processedLinks.has(a) && LINK_RE.test(a.href)) processLink(a);
        }
        if (root instanceof HTMLAnchorElement && root.href && LINK_RE.test(root.href)) {
            processLink(root);
        }
    }

    // ── MutationObserver ──────────────────────────────────────
    function onMutations(mutations) {
        for (var i = 0; i < mutations.length; i++) {
            var added = mutations[i].addedNodes;
            for (var j = 0; j < added.length; j++) {
                if (added[j].nodeType === Node.ELEMENT_NODE) scanForLinks(added[j]);
            }
        }
    }

    // ── Init ──────────────────────────────────────────────────
    injectCSS();

    dbg('Context: ' + location.href);
    dbg('Title: "' + document.title + '"');
    dbg('Body children: ' + (document.body ? document.body.children.length : 0));

    if (document.body) {
        var observer = new MutationObserver(onMutations);
        observer.observe(document.body, { childList: true, subtree: true });

        var existing = document.querySelectorAll('a[href]');
        dbg('Started -- ' + existing.length + ' existing link(s)');
        scanForLinks(document.body);
    } else {
        dbg('No document.body yet, deferring...');
        document.addEventListener('DOMContentLoaded', function() {
            injectCSS();
            var observer = new MutationObserver(onMutations);
            observer.observe(document.body, { childList: true, subtree: true });
            scanForLinks(document.body);
            dbg('Started (deferred) -- ' + document.querySelectorAll('a[href]').length + ' link(s)');
        });
    }
})();
