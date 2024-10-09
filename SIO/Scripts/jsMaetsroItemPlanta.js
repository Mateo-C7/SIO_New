function valideKey(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace ó tab
    if (key == 8 || key == 9 || key == 13 || key == 127) return true

    // 0-9 a partir del .decimal  
    if (field.value != "") {
        if ((field.value.indexOf(".")) > 0) {
            //si tiene un punto valida dos digitos en la parte decimal
            if (key > 47 && key < 58) {
                if (field.value == "") return true
                regexp = /[0-9]{2}$/
                return !(regexp.test(field.value))
            }
        }
    }
    // 0-9 
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[0-9]{16}?/
        return !(regexp.test(field.value))
    }
    // .
    if (key == 46) {
        if (field.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(field.value)
    }
    // other key
    return false
}
function validedecimal(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace ó tab
    if (key == 8 || key == 9 || key == 13 || key == 127) return true

    // 0-9 a partir del .decimal  
    if (field.value != "") {
        if ((field.value.indexOf(".")) > 0) {
            //si tiene un punto valida dos digitos en la parte decimal
            if (key > 47 && key < 58) {
                if (field.value == "") return true
                regexp = /[0-9]{4}$/
                return !(regexp.test(field.value))
            }
        }
    }
    // 0-9 
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[0-9]{6}?/
        return !(regexp.test(field.value))
    }
    // .
    if (key == 46) {
        if (field.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(field.value)
    }
    // other key
    return false
}
function valideKeyenteros(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace ó tab
    if (key == 8 || key == 9 || key == 13 || key == 127) return true
    // 0-9 
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /[0-9]{20}?/
        return !(regexp.test(field.value))
    }
    
    return false
}
function Navigate() {
    javascript: window.open("ItemReporte.aspx");
}




