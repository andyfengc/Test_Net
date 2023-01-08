window.store = {
    Canada_En: { url: "https://ca.kobobooks.com", pattern: /ca\.kobo/i }
    ,Canada_Fr: { url: "https://ca-fr.kobobooks.com", pattern: /ca-fr\.kobo/i}
    ,UnitedStates: { url: "https://us.kobobooks.com", pattern: /us\.kobo/i }
    ,UnitedKingdom: { url: "https://uk.kobobooks.com", pattern: /uk\.kobo/i }
    ,Italy: { url: "https://it.kobobooks.com", pattern: /it\.kobo/i }
    ,Spain: { url: "https://es.kobobooks.com", pattern: /es\.kobo/i }
    ,Germany: { url: "https://de.kobobooks.com", pattern: /de\.kobo/i }
    ,France: { url: "https://fr.kobobooks.com", pattern: /fr\.kobo/i }
    ,Portugal: { url: "https://pt.kobobooks.com", pattern: /pt\.kobo/i }
    ,Australia: { url: "https://au.kobobooks.com", pattern: /au\.kobo/i }
    ,NewZealand: { url: "https://nz.kobobooks.com", pattern: /nz\.kobo/i }
    ,rkbtest:{ url: "https://checkout.shopify", pattern: /checkout\.shopify/i }
    ,localhost:{ url: "http://localhost", pattern: /localhost:/i }
}
window.getStore = function(){
    var url = window.location.href;
    if (store.Canada_En.pattern.test(url)) return store.Canada_En;
    if (store.Canada_Fr.pattern.test(url)) return store.Canada_Fr;
    if (store.UnitedStates.pattern.test(url)) return store.UnitedStates;
    if (store.Germany.pattern.test(url)) return store.Germany;
    if (store.Spain.pattern.test(url)) return store.Spain;
    if (store.Italy.pattern.test(url)) return store.Italy;
    if (store.Portugal.pattern.test(url)) return store.Portugal;
    if (store.France.pattern.test(url)) return store.France;
    if (store.Australia.pattern.test(url)) return store.Australia;
    if (store.NewZealand.pattern.test(url)) return store.NewZealand;
    if (store.UnitedKingdom.pattern.test(url)) return store.UnitedKingdom;
    if (store.RkbTest.pattern.test(url)) return store.Canada_En;
    if (store.Localhost.pattern.test(url)) return store.NewZealand;
}
window.getCountryCode = function() {
    switch (getStore()) {
        case store.UnitedStates: return "US";
        case store.Germany: return "DE";
        case store.Spain: return "ES";
        case store.Italy: return "IT";
        case store.Portugal: return "PT";
        case store.France: return "FR";
        case store.Australia: return "AU";
        case store.NewZealand: return "NZ";
        case store.UnitedKingdom: return "GB";
        case store.Canada_En: 
        case store.Canada_Fr:
        default: return "CA";
    }
}
window.getCountryLanguageCode = function(){
    switch (getStore()) {
        case store.Canada_Fr: 
        case store.France: return "fr";
        case store.Germany: return "de";
        case store.Spain: return "es";
        case store.Italy: return "it";
        case store.Portugal: return "pt-PT";
        case store.Australia: 
        case store.NewZealand: return "en-AU";
        case store.UnitedKingdom: return "en-GB";
        case store.Canada_En: 
        case store.UnitedStates: 
        default: return "en";
    }
}
if (getStore() == store.UnitedStates)
{
    var text = document.getElementById("checkout_shipping_address_address1");
    text.parentNode.innerHTML += '<p id="message" class="field__error-message warning"></p>';
    var message = document.getElementById("message");
    message.innerHTML = "Kobo does not ship to PO Boxes and will require a street address for delivery";
    var text = document.getElementById("checkout_shipping_address_address1"),
    poBoxText = "PO BOX;POBOX;P O BOX;P OBOX;P.O.BOX;P.O BOX;P.OBOX;P.O. BOX;P. O. BOX;PO.BOX;P O.BOX;P O. BOX;PO. BOX".split(";"),
    show = !1,
    showText = !1;
    "field field--required field--error" == text.parentNode.className && (show = !0);
    message.style.display = 1 == showText ? "display" : "none";
}
var Max_Length = 35;
var fields = "checkout_shipping_address_first_name checkout_shipping_address_last_name checkout_shipping_address_address1 checkout_shipping_address_address2 checkout_shipping_address_city checkout_shipping_address_zip checkout_shipping_address_phone checkout_billing_address_first_name checkout_billing_address_last_name checkout_billing_address_address1 checkout_billing_address_address2 checkout_billing_address_city checkout_billing_address_zip checkout_billing_address_phone".split(" ");
for (i = 0; i < fields.length; i++) {
    if (document.getElementById(fields[i])) document.getElementById(fields[i]).maxLength = Max_Length;
    }
var componentForm = [
"checkout_shipping_address_city",
"checkout_shipping_address_zip",
    "checkout_shipping_address_province"
    ];
    var placeSearch, autocomplete;
var componentFormMapping = {
    street_number: 'short_name',
    route: 'long_name',
    locality: 'long_name',
    administrative_area_level_1: 'long_name', // province
    country: 'long_name',
    postal_code: 'short_name'
};
if (getStore() == store.Spain){
    delete componentFormMapping.administrative_area_level_1;
    componentFormMapping["administrative_area_level_2"] = 'long_name';
}
if (getStore() == store.Italy){
    delete componentFormMapping.administrative_area_level_1;
    componentFormMapping["administrative_area_level_3"] = 'long_name';
}
function isPrintableKey(e) {
    var keycode = e.keyCode;
    var valid =
        (keycode > 47 && keycode < 58) || // number keys
        keycode == 32 || // spacebar & return key(s) (if you want to allow carriage returns)
        (keycode > 64 && keycode < 91) || // letter keys
        (keycode > 95 && keycode < 112) || // numpad keys
        (keycode > 185 && keycode < 193) || // ;=,-./` (in order)
        (keycode > 218 && keycode < 223); // [\]' (in order)
        return valid;
    }
    function isNonPrintableKey(e) {
        return !isPrintableKey(e);
    }
    function isCombinationKeyPressed(e) {
        return e.ctrlKey || e.altKey || e.shiftKey || e.metaKey;
    }
    function selectItemByValue(select, value) {
        for (var i = 0; i < select.options.length; i++) {
            if (select.options[i].value === value) {
                select.selectedIndex = i;
                break;
            }
        }
    }
function getMaxLengthMessage(){
    switch (getStore()) {
        case store.Canada_Fr: 
        case store.France: return " caractères restants (" + Max_Length + " maximum)";
        case store.Germany: return " Zeichen möglich (" + Max_Length + " maximal)";
        case store.Spain: return " caracteres restantes (" + Max_Length + " máximo)";
        case store.Italy: return " caratteri rimanenti (" + Max_Length + " al massimo)";
        case store.Portugal: return " caracteres restantes (" + Max_Length + " no máximo)";
        case store.Australia: return " characters remaining (" + Max_Length + " maximum)";
        case store.NewZealand: return " characters remaining (" + Max_Length + " maximum)";
        case store.UnitedKingdom: return " characters remaining (" + Max_Length + " maximum)";
        case store.Canada_En: 
        case store.UnitedStates: 
        default: return " characters remaining (" + Max_Length + " maximum)";
    }
}
function getMaxLengthReachMessage(){
    switch (getStore()) {
        case store.Canada_Fr: 
        case store.France: return "la longueur maximum de <b>" + Max_Length + "</b> caractères est atteint";
        case store.Germany: return "die maximale Länge <b>" + Max_Length + "</b> Zeichen erreicht ist";
        case store.Spain: return "la longitud máxima de <b>" + Max_Length + "</b> caracteres se alcanza";
        case store.Italy: return "la lunghezza massima di <b>" + Max_Length + "</b> personaggi è raggiunto";
        case store.Portugal: return "o comprimento máximo de <b>" + Max_Length + "</b> caracteres é atingido";
        case store.Australia: return "the max length of <b>" + Max_Length + "</b> characters is reached";
        case store.NewZealand: return "the max length of <b>" + Max_Length + "</b> characters is reached";
        case store.UnitedKingdom: return "the max length of <b>" + Max_Length + "</b> characters is reached";
        case store.Canada_En: 
        case store.UnitedStates: 
        default: return "the max length of <b>" + Max_Length + "</b> characters is reached";
    }
}
function validateMaxLength(e) {
    var currentValue = document.getElementById("checkout_shipping_address_address1").value;
    var currentLength;
    if (e) { // is a event
        if (e.type == "keypress" || e.type == "keydown") {
            if (e.keyCode == '8') {
                if (currentValue.length == 0) {
                    currentLength = currentValue.length;
                } else {
                    currentLength = currentValue.length - 1;
                }
            } else if (isNonPrintableKey(e) || isCombinationKeyPressed(e)) {
                currentLength = currentValue.length;
            } else { // printable key
                currentLength = currentValue.length + 1;
            }
        } else {
            currentLength = currentValue.length;
        }
    } else {
        currentLength = currentValue.length;
    }
    var maxLength = document.getElementById("maxLength");
    if (currentLength > Max_Length) {
        address1 = document.getElementById("checkout_shipping_address_address1");
        address1.parentNode.className = "field field--required field--error field--focus";
        maxLength.innerHTML = getMaxLengthReachMessage();
    } else {
        address1 = document.getElementById("checkout_shipping_address_address1");
        address1.parentNode.className = "field field--required";
        var currentLeftLength = Max_Length - currentLength;
        maxLength.innerHTML = currentLeftLength + getMaxLengthMessage();
    }
};
var address1 = document.getElementById("checkout_shipping_address_address1");
address1.parentNode.innerHTML = address1.parentNode.innerHTML + "<p id=\"maxLength\" class=\"field__error-message warning\"></p>";
var maxLength = document.getElementById("maxLength");
address1 = document.getElementById("checkout_shipping_address_address1");
address1.maxLength = Max_Length;
maxLength.style.display = "block";
var currentLeftLength = Max_Length;
maxLength.innerHTML = currentLeftLength + getMaxLengthMessage();
address1.onkeydown = validateMaxLength;
address1.onblur = validateMaxLength;
if (getStore() == store.UnitedStates) {//The PO Box verification is need it only for the US store
    var text = document.getElementById("checkout_shipping_address_address1");
    var message = document.getElementById("message");
    text.onkeyup = function(a) {
        showText = !1;
        for (i = 0; i < poBoxText.length; i++)
            if (-1 != text.value.toLowerCase().indexOf(poBoxText[i].toLowerCase())) {
                showText = !0;
                break
            }
            1 == showText ? (text.parentNode.className = "field field--required field--error field--focus", message.style.display = "block") : (text.parentNode.className = show ? "field field--required field--error field--focus" : "field field--required field--focus", message.style.display = "none")
        };
        for (i = 0; i < poBoxText.length; i++)
            if (-1 != text.value.toLowerCase().indexOf(poBoxText[i].toLowerCase())) {
                showText = !0;
                break
            }
        }
window.initAutocomplete = function() {
    var options = {
        types: ['address'],
        componentRestrictions: {
            country: getCountryCode()
        }
    };
    if (options.componentRestrictions.country == "") {
        delete options.componentRestrictions;
    }
    var address1 = document.getElementById("checkout_shipping_address_address1");
    google.maps.event.addDomListener(address1, 'keydown', function(e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    });
    autocomplete = new google.maps.places.Autocomplete((document.getElementById('checkout_shipping_address_address1')), options);
    autocomplete.addListener('place_changed', fillInAddress);
}
function fillInAddress() {
    var place = autocomplete.getPlace();
    for (var index in componentForm) {
        document.getElementById(componentForm[index]).value = '';
        document.getElementById(componentForm[index]).disabled = false;
    }
    var country, province, city, postalCode;
    var street = "";
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentFormMapping[addressType]) {
            var val = place.address_components[i][componentFormMapping[addressType]];
            if (addressType == "street_number") {
                street += val;
            }
            if (addressType == "route") {
                if (street != "") {
                    street += " " + val;
                } else {
                    street += val;
                }
            }
            if (addressType == "locality") {
                city = val;
            } else if (addressType == "postal_code") {
                postalCode = val;
            } else if (addressType == "country") {
                country = val;
            } else {
                province = val;
            }
        }
    }
    if (street) {
        document.getElementById("checkout_shipping_address_address1").value = street;
    }
    if (city) {
        document.getElementById("checkout_shipping_address_city").value = city;
    }
    if (postalCode) {
        document.getElementById("checkout_shipping_address_zip").value = postalCode;
    }
    if (province) {
        if (province == "Québec") {
            province = "Quebec";
        }
        selectItemByValue(document.getElementById("checkout_shipping_address_province"), province);
    }
    validateMaxLength();
    document.activeElement.blur();
}
var googleMapEventListenerScript = document.createElement('script');
googleMapEventListenerScript.async = true;
googleMapEventListenerScript.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAwO_kj1b7YdHj_Gp567LepbrRGW4s-n4s&signed_in=true&libraries=places&callback=initAutocomplete&language="+getCountryLanguageCode();
document.getElementsByTagName('head')[0].appendChild(googleMapEventListenerScript);