(function () {
    if (!window.lswAntDesignThemeSettings) {
        window.lswAntDesignThemeSettings = {};
    }

    const registry = {};
    const themeClasses = [
        "lsw-pro-theme",
        "lsw-pro-theme-light",
        "lsw-pro-theme-dark",
        "lsw-pro-theme-real-dark",
        "colorWeak"
    ];

    function clamp(value, min, max) {
        return Math.min(Math.max(value, min), max);
    }

    function clearFadeTimer(state) {
        if (state.fadeTimer) {
            window.clearTimeout(state.fadeTimer);
            state.fadeTimer = null;
        }
    }

    function scheduleFade(state) {
        clearFadeTimer(state);
        state.fadeTimer = window.setTimeout(() => {
            if (!state.host.classList.contains("lsw-theme-setting-fab-active")) {
                state.host.classList.add("lsw-theme-setting-fab-faded");
            }
        }, 1200);
    }

    function snapToRight(state, keepTop) {
        const host = state.host;
        const rect = host.getBoundingClientRect();
        const width = rect.width || 48;
        const top = keepTop ? rect.top : window.innerHeight * 0.6;
        const clampedTop = clamp(top, 24, window.innerHeight - rect.height - 24);

        host.style.left = "";
        host.style.right = `${Math.round(-width / 2)}px`;
        host.style.top = `${Math.round(clampedTop)}px`;
    }

    function initialize(id) {
        const host = document.getElementById(id);
        if (!host || registry[id]) {
            return;
        }

        const handle = host;

        const state = {
            host: host,
            pointerId: null,
            startX: 0,
            startY: 0,
            originX: 0,
            originY: 0,
            moved: false,
            fadeTimer: null
        };

        const onPointerDown = function (evt) {
            state.pointerId = evt.pointerId;
            state.startX = evt.clientX;
            state.startY = evt.clientY;
            const rect = host.getBoundingClientRect();
            state.originX = rect.left;
            state.originY = rect.top;
            state.moved = false;
            host.classList.remove("lsw-theme-setting-fab-faded");
            clearFadeTimer(state);
            handle.setPointerCapture(evt.pointerId);
        };

        const onPointerMove = function (evt) {
            if (state.pointerId !== evt.pointerId) {
                return;
            }

            const dx = evt.clientX - state.startX;
            const dy = evt.clientY - state.startY;
            if (Math.abs(dx) > 3 || Math.abs(dy) > 3) {
                state.moved = true;
            }

            if (!state.moved) {
                return;
            }

            const rect = host.getBoundingClientRect();
            const nextLeft = clamp(state.originX + dx, 0, window.innerWidth - rect.width);
            const nextTop = clamp(state.originY + dy, 24, window.innerHeight - rect.height - 24);

            host.style.right = "";
            host.style.left = `${Math.round(nextLeft)}px`;
            host.style.top = `${Math.round(nextTop)}px`;
        };

        const onPointerUp = function (evt) {
            if (state.pointerId !== evt.pointerId) {
                return;
            }

            state.pointerId = null;
            if (state.moved) {
                snapToRight(state, true);
            }
            scheduleFade(state);
        };

        const onMouseEnter = function () {
            host.classList.remove("lsw-theme-setting-fab-faded");
            clearFadeTimer(state);
        };

        const onMouseLeave = function () {
            scheduleFade(state);
        };

        const onWindowBlur = function () {
            if (!host.classList.contains("lsw-theme-setting-fab-active")) {
                host.classList.add("lsw-theme-setting-fab-faded");
            }
        };

        const onWindowFocus = function () {
            host.classList.remove("lsw-theme-setting-fab-faded");
        };

        handle.addEventListener("pointerdown", onPointerDown);
        handle.addEventListener("pointermove", onPointerMove);
        handle.addEventListener("pointerup", onPointerUp);
        handle.addEventListener("pointercancel", onPointerUp);
        host.addEventListener("mouseenter", onMouseEnter);
        host.addEventListener("mouseleave", onMouseLeave);
        window.addEventListener("blur", onWindowBlur);
        window.addEventListener("focus", onWindowFocus);

        registry[id] = {
            state: state,
            onPointerDown: onPointerDown,
            onPointerMove: onPointerMove,
            onPointerUp: onPointerUp,
            onMouseEnter: onMouseEnter,
            onMouseLeave: onMouseLeave,
            onWindowBlur: onWindowBlur,
            onWindowFocus: onWindowFocus
        };

        snapToRight(state, false);
        scheduleFade(state);
    }

    function dispose(id) {
        const entry = registry[id];
        if (!entry) {
            return;
        }

        const host = entry.state.host;
        const handle = host;

        if (handle) {
            handle.removeEventListener("pointerdown", entry.onPointerDown);
            handle.removeEventListener("pointermove", entry.onPointerMove);
            handle.removeEventListener("pointerup", entry.onPointerUp);
            handle.removeEventListener("pointercancel", entry.onPointerUp);
        }

        host.removeEventListener("mouseenter", entry.onMouseEnter);
        host.removeEventListener("mouseleave", entry.onMouseLeave);
        window.removeEventListener("blur", entry.onWindowBlur);
        window.removeEventListener("focus", entry.onWindowFocus);
        clearFadeTimer(entry.state);
        delete registry[id];
    }

    function applyThemeClass(themeClass) {
        const body = document.body;
        if (!body) {
            return;
        }

        themeClasses.forEach((name) => body.classList.remove(name));
        if (themeClass && typeof themeClass === "string") {
            themeClass.split(" ")
                .map((item) => item.trim())
                .filter((item) => item.length > 0)
                .forEach((item) => body.classList.add(item));
        }
    }

    window.lswAntDesignThemeSettings.initialize = initialize;
    window.lswAntDesignThemeSettings.dispose = dispose;
    window.lswAntDesignThemeSettings.applyThemeClass = applyThemeClass;
})();
