// Uprooted NSFW Content Filter
// Injected into DotNetBrowser main frame via ExecuteJavaScript.
// Uses MutationObserver to watch for <img> elements, classifies via
// Google Cloud Vision SafeSearch API, and applies CSS blur to flagged content.
(function() {
    'use strict';

    // Self-guard: don't inject twice
    if (window.__UPROOTED_NSFW_ACTIVE__) return;
    window.__UPROOTED_NSFW_ACTIVE__ = true;

    // Read config (injected as window.__UPROOTED_NSFW_CONFIG__ by C# hook or HTML patch)
    var config = window.__UPROOTED_NSFW_CONFIG__ || {};
    if (!config.enabled || !config.apiKey) {
        window.__UPROOTED_NSFW_ACTIVE__ = false;
        return;
    }

    var API_KEY = config.apiKey;
    var THRESHOLD = typeof config.threshold === 'number' ? config.threshold : 0.6;
    var MIN_IMAGE_SIZE = 50; // Skip images smaller than 50px

    // Confidence mapping: SafeSearch likelihood -> numeric score
    var LIKELIHOOD_SCORES = {
        'VERY_UNLIKELY': 0,
        'UNLIKELY': 0.2,
        'POSSIBLE': 0.4,
        'LIKELY': 0.6,
        'VERY_LIKELY': 0.8
    };

    // Rate limiting
    var MAX_CONCURRENT = 5;
    var MAX_PER_MINUTE = 30;
    var activeConcurrent = 0;
    var minuteCount = 0;
    var pendingQueue = [];

    // Reset minute counter every 60 seconds
    setInterval(function() { minuteCount = 0; processQueue(); }, 60000);

    // URL cache: Map<normalizedUrl, isSafe>
    var urlCache = new Map();
    var URL_CACHE_MAX = 2000;
    var processedImages = new WeakSet();

    // Inject CSS
    var style = document.createElement('style');
    style.textContent = [
        '.uprooted-nsfw-pending { filter: blur(8px) !important; transition: filter 0.3s ease; }',
        '.uprooted-nsfw-blur { filter: blur(25px) !important; transition: filter 0.3s ease; }',
        '.uprooted-nsfw-revealed { filter: none !important; transition: filter 0.3s ease; }',
        '.uprooted-nsfw-wrapper { position: relative; display: inline-block; }',
        '.uprooted-nsfw-overlay {',
        '  position: absolute; top: 0; left: 0; width: 100%; height: 100%;',
        '  display: flex; align-items: center; justify-content: center;',
        '  background: rgba(0, 0, 0, 0.6); cursor: pointer; z-index: 10;',
        '  border-radius: 4px;',
        '}',
        '.uprooted-nsfw-overlay-text {',
        '  color: #f2f2f2; font-size: 13px; text-align: center; padding: 12px;',
        '  background: var(--color-background-secondary, rgba(15, 25, 35, 0.9));',
        '  border-radius: 8px; max-width: 80%;',
        '}',
        '.uprooted-nsfw-overlay-text .nsfw-label {',
        '  color: var(--color-error, #C42B1C); font-weight: 600; font-size: 12px;',
        '  text-transform: uppercase; letter-spacing: 0.5px; margin-bottom: 4px;',
        '}',
        '.uprooted-nsfw-revealed + .uprooted-nsfw-overlay { display: none; }'
    ].join('\n');
    document.head.appendChild(style);

    function normalizeUrl(url) {
        try {
            var u = new URL(url);
            u.search = '';
            return u.href;
        } catch(e) {
            return url;
        }
    }

    function processQueue() {
        while (pendingQueue.length > 0 && activeConcurrent < MAX_CONCURRENT && minuteCount < MAX_PER_MINUTE) {
            var next = pendingQueue.shift();
            classifyImage(next.img, next.url);
        }
    }

    function enqueueClassification(img, url) {
        if (activeConcurrent < MAX_CONCURRENT && minuteCount < MAX_PER_MINUTE) {
            classifyImage(img, url);
        } else {
            pendingQueue.push({ img: img, url: url });
        }
    }

    function classifyImage(img, url) {
        activeConcurrent++;
        minuteCount++;

        var normalizedUrl = normalizeUrl(url);

        fetch('https://vision.googleapis.com/v1/images:annotate?key=' + API_KEY, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                requests: [{
                    image: { source: { imageUri: url } },
                    features: [{ type: 'SAFE_SEARCH_DETECTION' }]
                }]
            })
        })
        .then(function(resp) { return resp.json(); })
        .then(function(data) {
            activeConcurrent--;

            var isSafe = true;
            try {
                var annotation = data.responses[0].safeSearchAnnotation;
                if (annotation) {
                    var adultScore = LIKELIHOOD_SCORES[annotation.adult] || 0;
                    var violenceScore = LIKELIHOOD_SCORES[annotation.violence] || 0;
                    var racyScore = LIKELIHOOD_SCORES[annotation.racy] || 0;
                    var maxScore = Math.max(adultScore, violenceScore, racyScore);
                    isSafe = maxScore < THRESHOLD;
                }
            } catch(e) {
                // Parse error -- fail open (show image)
            }

            // Cache result
            if (urlCache.size >= URL_CACHE_MAX) {
                // Evict oldest entry
                var firstKey = urlCache.keys().next().value;
                urlCache.delete(firstKey);
            }
            urlCache.set(normalizedUrl, isSafe);

            applyResult(img, isSafe);
            processQueue();
        })
        .catch(function() {
            // Network/API error -- fail open (show image)
            activeConcurrent--;
            applyResult(img, true);
            processQueue();
        });
    }

    function applyResult(img, isSafe) {
        img.classList.remove('uprooted-nsfw-pending');
        if (isSafe) {
            // Safe -- no blur needed
            return;
        }

        // NSFW -- apply full blur and add overlay
        img.classList.add('uprooted-nsfw-blur');

        // Wrap image if not already wrapped
        if (!img.parentElement || !img.parentElement.classList.contains('uprooted-nsfw-wrapper')) {
            var wrapper = document.createElement('div');
            wrapper.className = 'uprooted-nsfw-wrapper';
            img.parentElement.insertBefore(wrapper, img);
            wrapper.appendChild(img);

            var overlay = document.createElement('div');
            overlay.className = 'uprooted-nsfw-overlay';
            overlay.innerHTML = '<div class="uprooted-nsfw-overlay-text">' +
                '<div class="nsfw-label">Sensitive Content</div>' +
                'Click to reveal</div>';

            overlay.addEventListener('click', function(e) {
                e.stopPropagation();
                e.preventDefault();
                if (img.classList.contains('uprooted-nsfw-revealed')) {
                    // Re-blur
                    img.classList.remove('uprooted-nsfw-revealed');
                    img.classList.add('uprooted-nsfw-blur');
                    overlay.style.display = '';
                } else {
                    // Reveal
                    img.classList.remove('uprooted-nsfw-blur');
                    img.classList.add('uprooted-nsfw-revealed');
                    overlay.style.display = 'none';
                }
            });

            wrapper.appendChild(overlay);
        }
    }

    function processImage(img) {
        // Skip if already processed
        if (processedImages.has(img)) return;
        processedImages.add(img);

        var src = img.src || img.currentSrc;
        if (!src) return;

        // Skip data: URIs
        if (src.startsWith('data:')) return;

        // Skip blob: URIs
        if (src.startsWith('blob:')) return;

        // Skip tiny images (icons, avatars, etc.)
        if (img.naturalWidth > 0 && img.naturalWidth < MIN_IMAGE_SIZE) return;
        if (img.naturalHeight > 0 && img.naturalHeight < MIN_IMAGE_SIZE) return;

        // Check dimensions via attributes if natural dimensions not available
        var width = img.width || parseInt(img.getAttribute('width')) || 0;
        var height = img.height || parseInt(img.getAttribute('height')) || 0;
        if (width > 0 && width < MIN_IMAGE_SIZE && height > 0 && height < MIN_IMAGE_SIZE) return;

        var normalizedUrl = normalizeUrl(src);

        // Check cache first
        if (urlCache.has(normalizedUrl)) {
            var cached = urlCache.get(normalizedUrl);
            if (!cached) {
                // Known NSFW -- apply immediately
                applyResult(img, false);
            }
            return;
        }

        // Apply pre-blur while classifying
        img.classList.add('uprooted-nsfw-pending');

        // Enqueue classification
        enqueueClassification(img, src);
    }

    // Process all existing images
    var existingImages = document.querySelectorAll('img');
    for (var i = 0; i < existingImages.length; i++) {
        processImage(existingImages[i]);
    }

    // Watch for new images via MutationObserver
    var observer = new MutationObserver(function(mutations) {
        for (var i = 0; i < mutations.length; i++) {
            var addedNodes = mutations[i].addedNodes;
            for (var j = 0; j < addedNodes.length; j++) {
                var node = addedNodes[j];
                if (node.nodeType !== 1) continue; // Element nodes only

                if (node.tagName === 'IMG') {
                    processImage(node);
                } else if (node.querySelectorAll) {
                    var imgs = node.querySelectorAll('img');
                    for (var k = 0; k < imgs.length; k++) {
                        processImage(imgs[k]);
                    }
                }
            }
        }
    });

    observer.observe(document.body, {
        childList: true,
        subtree: true
    });

    // Handle images that load their src lazily (after being added to DOM)
    document.addEventListener('load', function(e) {
        if (e.target && e.target.tagName === 'IMG') {
            processImage(e.target);
        }
    }, true);

    console.log('[Uprooted] NSFW content filter active (threshold: ' + THRESHOLD + ')');
})();
