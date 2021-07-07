BlazorMergely = {
    init: function (htmlId, optionsStr) {
        CodeMirror.modeURL = "../_content/com.github.akovac35.BlazorMergely/codemirror/mode/%N/%N.js";

        var optionsObj = null;
        if (!!optionsStr) {
            optionsObj = JSON.parse(optionsStr);
        }

        if (!!optionsObj) {
            $(htmlId).mergely(optionsObj);

            if (!!optionsObj.cmsettings && !!optionsObj.cmsettings.mode) {
                this.changeLangDisplayMode(htmlId, optionsObj.cmsettings.mode);
            }
        } else {
            $(htmlId).mergely();
        }
    },
    
    changeLangDisplayMode: function (htmlId, modeStr) {
        var tmp = this.getLangDisplayModeFromString(modeStr);
        if (tmp) {
            var options = $(htmlId).mergely('options');
            if (!options.cmsettings) options.cmsettings = {};
            options.cmsettings.mode = tmp.spec;
            $(htmlId).mergely('options', options);

            CodeMirror.autoLoadMode($(htmlId).mergely('cm', 'lhs'), tmp.mode);
            CodeMirror.autoLoadMode($(htmlId).mergely('cm', 'rhs'), tmp.mode);

            $(htmlId).mergely('cm', 'lhs').setOption('mode', tmp.spec); // Changing mergely options does not work, setting editor options instead
            $(htmlId).mergely('cm', 'rhs').setOption('mode', tmp.spec); // Changing mergely options does not work, setting editor options instead
        } else {
            var options = $(htmlId).mergely('options');
            if (!options.cmsettings) options.cmsettings = {};
            options.cmsettings.mode = null;
            $(htmlId).mergely('options', options);

            $(htmlId).mergely('cm', 'lhs').setOption('mode', null); // Changing mergely options does not work, setting editor options instead
            $(htmlId).mergely('cm', 'rhs').setOption('mode', null); // Changing mergely options does not work, setting editor options instead
        }
    },

    // modeValue can be filename, mime, or mode name.
    getLangDisplayModeFromString: function (modeStr) {
        var val = modeStr;
        var m = null;
        var mode = null;
        var spec = null;

        if (m = /.+\.([^.]+)$/.exec(val)) {
            var info = CodeMirror.findModeByExtension(m[1]);
            if (info) {
                mode = info.mode;
                spec = info.mime;
            }
        } else if (/\//.test(val)) {
            var info = CodeMirror.findModeByMIME(val);
            if (info) {
                mode = info.mode;
                spec = val;
            }
        } else {
            mode = spec = val;
        }
        if (mode) {
            return { spec: spec, mode: mode };
        } else {
            console.warn("Could not find a CodeMirror mode corresponding to '" + val + "'.");
            return null;
        }
    },

    getOptions: function (htmlId) {
        var options = JSON.stringify($(htmlId).mergely('options'));
        return options;        
    },

    setOptions: function (htmlId, optionsStr) {
        var options = JSON.parse(optionsStr);
        $(htmlId).mergely('options', options);
    },

    clear: function (htmlId) {
        $(htmlId).mergely('clear', 'lhs');
        $(htmlId).mergely('clear', 'rhs');
    },

    get: function (htmlId, sideStr) {
        return $(htmlId).mergely('get', sideStr);
    },

    getBoth: function (htmlId) {
        var left = $(htmlId).mergely('get', 'lhs');
        var right = $(htmlId).mergely('get', 'rhs');

        return [left, right];
    },

    set: function (htmlId, lhsValueStr, rhsValueStr) {
        $(htmlId).mergely('lhs', lhsValueStr);
        $(htmlId).mergely('lhs', lhsValueStr); // For some reason this needs to be set two times when lhs and rhs are set one after another
        $(htmlId).mergely('rhs', rhsValueStr);
    },

    lhs: function (htmlId, valueStr) {
        $(htmlId).mergely('lhs', valueStr);
    },

    rhs: function (htmlId, valueStr) {
        $(htmlId).mergely('rhs', valueStr);
    },

    update: function (htmlId) {
        $(htmlId).mergely('update');
    },

    toggleLineWrapping: function (htmlId) {
        var options = $(htmlId).mergely('options');
        options.wrap_lines = !options.wrap_lines;
        $(htmlId).mergely('options', options);
    }
}

