var idiomaSeleccionado = 'es';

$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    ShowComboTranslation();
    $.i18n({
        locale: idiomaSeleccionado //'es'
    });
    cambiarIdioma();
});


function EndRequestHandler(sender, args) {
    ShowComboTranslation();
    cambiarIdioma();

}

function ShowComboTranslation() {
    $("select option").each(function (index) {
        $(this).attr('data-i18n', '[html]' + $(this).text().replace(/[^-A-Za-z0-9]+/g, '_').toLowerCase());
        console.log("'" + $(this).text().replace(/[^-A-Za-z0-9]+/g, '_').toLowerCase() + "':'" + $(this).text() + "'");
    });

}

function cambiarIdioma() {
    $.i18n().load({
        en: './Scripts/languages/obra_languages_en.json',
        es: './Scripts/languages/obra_languages_es.json',
        br: './Scripts/languages/obra_languages_br.json'
    }).done(function () {
        ReloadLanguage();
    });

    $(".langes").click(function () {
        idiomaSeleccionado = 'es';
        SaveLanguage(idiomaSeleccionado);
        $.i18n({
            locale: 'es'
        });
        $.fn.i18n();
    });
    $(".langbr").click(function () {
        idiomaSeleccionado = 'br';
        SaveLanguage(idiomaSeleccionado);
        $.i18n({
            locale: 'br'
        });
        $.fn.i18n();
    });
    $(".langen").click(function () {
        idiomaSeleccionado = 'en';
        SaveLanguage(idiomaSeleccionado);
        $.i18n({
            locale: 'en'
        });
        $.fn.i18n();
    });
}

function SaveLanguage(lang) {
    if (typeof sessionStorage !== 'undefined') {
        try {
            sessionStorage.setItem('obralangFUP', lang);
        } catch (e) {
            // localStorage is disabled
        }
    } else {
        // localStorage is not available
    }
}

function getLanguage() {
    if (typeof sessionStorage !== 'undefined') {
        try {
            return sessionStorage.getItem('obralangFUP');
        } catch (e) {
            return "es";
        }
    } else {
        return "es";
    }
}

function ReloadLanguage() {
    var lang = getLanguage();
    if (lang == undefined || lang == null) {
        lang = "es";
    }
    switch (lang) {
        case "es":
            $(".langes").click();
            break;
        case "en":
            $(".langen").click();
            break;
        case "br":
            $(".langbr").click();
            break;
    }
}