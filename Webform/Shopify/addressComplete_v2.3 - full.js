(function(window){    
    // get current store
    window.getStore = function(){
        return getStoreByUrl("https://us.kobobooks.com/");
        //return getStoreByUrl(window.location.href);
    }

    // get country code
    window.getCountryCode = function() {
        // way1, get country code from country selector
        // set default country
        //document.getElementById("checkout_shipping_address_country").selectedIndex = 229;
        // var select = document.getElementById("checkout_shipping_address_country");
        // var country = select.options[select.selectedIndex].value;
        // switch (country) {
        //     case "Canada":
        //         return "CA";
        //     case "United States":
        //         return "US";
        //     case "United Kingdom":
        //         return "GB";
        //     case "Italy":
        //         return "IT";
        //     case "France":
        //         return "FR";
        //     case "Spain":
        //         return "ES";
        //     case "Portugal":
        //         return "PT";
        //     case "Germany":
        //         return "DE";
        //     default: return "CA";
        // }

        // way2, get country code from url
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

    // get country language code
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

    // add 35 character limits for all fields
    var Max_Length = 35;
    var fields = "checkout_shipping_address_first_name checkout_shipping_address_last_name checkout_shipping_address_address1 checkout_shipping_address_address2 checkout_shipping_address_city checkout_shipping_address_zip checkout_shipping_address_phone checkout_billing_address_first_name checkout_billing_address_last_name checkout_billing_address_address1 checkout_billing_address_address2 checkout_billing_address_city checkout_billing_address_zip checkout_billing_address_phone".split(" ");
    for (i = 0; i < fields.length; i++) {
        if (document.getElementById(fields[i])) document.getElementById(fields[i]).maxLength = Max_Length;
    }

    // address control ids
    var componentForm = [
        "checkout_shipping_address_city",
        "checkout_shipping_address_zip",
        //"checkout_shipping_address_country",
        "checkout_shipping_address_province"
        ];

    var placeSearch, autocomplete;

    // address fields in google address autocomplete api
    var componentFormMapping = {
        street_number: 'short_name',
        route: 'long_name',
        locality: 'long_name',
        //administrative_area_level_1: 'short_name',
        administrative_area_level_1: 'long_name', // province
        country: 'long_name',
        postal_code: 'short_name'
    };

    // modify province filed because of different administrative level in some countries
    if (getStore() == store.Spain){
        delete componentFormMapping.administrative_area_level_1;
        componentFormMapping["administrative_area_level_2"] = 'long_name';
    }
    if (getStore() == store.Italy){
        delete componentFormMapping.administrative_area_level_1;
        componentFormMapping["administrative_area_level_3"] = 'long_name';
    }

       // add address maxlength message label
       var address1 = document.getElementById("checkout_shipping_address_address1");

    // set max size
    address1.maxLength = Max_Length;

    // register address maxlength events
    address1.onkeydown = validateMaxLength;
    address1.onblur = validateMaxLength;

    // add pobox verification for us store
    if (getStore() == store.UnitedStates)
    {  
        address1.onkeyup = validatePobox;
    }


    // common
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

    // the max length of 35 characters is reached
    function getMaxLengthReachMessage(){
        switch (getStore()) {
            case store.Canada_Fr: 
            case store.France: return "la longueur maximum de " + Max_Length + " caractères est atteint";
            case store.Germany: return "die maximale Länge " + Max_Length + " Zeichen erreicht ist";
            case store.Spain: return "la longitud máxima de " + Max_Length + " caracteres se alcanza";
            case store.Italy: return "la lunghezza massima di " + Max_Length + " personaggi è raggiunto";
            case store.Portugal: return "o comprimento máximo de " + Max_Length + " caracteres é atingido";
            case store.Australia: return "the max length of " + Max_Length + " characters is reached";
            case store.NewZealand: return "the max length of " + Max_Length + " characters is reached";
            case store.UnitedKingdom: return "the max length of " + Max_Length + " characters is reached";
            case store.Canada_En: 
            case store.UnitedStates: 
            default: return "the max length of " + Max_Length + " characters is reached";
        }
    }

    function removeIfExists(element){
        if (element){
            element.parentNode.removeChild(element);
        }
    }

    function validatePobox(e){
        // remove pobox message if exists
        removeIfExists(document.getElementById("poboxMessage"));
        // clear validation result if has any
        ClearValidationResult(validationResultKey.Address1);
        // validate
        if (isPobox(address1.value)){
            //address1.parentNode.setAttribute("class", "field__input-wrapper boxshadow field field--required field--error");
            address1.parentNode.setAttribute("class", "field field--error");
            address1.setAttribute("class", "boxshadow field--error field__input");
            var poboxMessage = document.createElement("p");
            poboxMessage.setAttribute("id", "poboxMessage");
            poboxMessage.setAttribute("class", "field__message field__message--error");
            poboxMessage.setAttribute("style", "display:block");
            var textNode = document.createTextNode("Kobo does not ship to PO Boxes and will require a street address for delivery");
            poboxMessage.appendChild(textNode);
            address1.parentNode.appendChild(poboxMessage);
            //e.preventDefault();
            AddValidationResult(validationResultKey.Address1, "wrong pobox number");
        }
    }


    function validateMaxLength(e) {
        // remove maxlength message if exists
        removeIfExists(document.getElementById("maxLength"));
        // validate
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

        if (currentLength > Max_Length) {
            // display max length over limitation message
            address1 = document.getElementById("checkout_shipping_address_address1");
            address1.parentNode.setAttribute("class", "field__input-wrapper field field--required field--error");
            var maxLengthMessage = document.createElement("p");
            maxLengthMessage.setAttribute("id", "maxLength");
            maxLengthMessage.setAttribute("class","field__message field__message--error");
            maxLengthMessage.setAttribute("style", "display:block");
            var textNode = document.createTextNode(getMaxLengthReachMessage());
            maxLengthMessage.appendChild(textNode);
            address1.parentNode.appendChild(maxLengthMessage);
        } 
    };


    // init google maps plugin
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

    // process address autocomplete dropdown select event
    function fillInAddress() {
        // Get the place details from the autocomplete object.
        var place = autocomplete.getPlace();

        for (var index in componentForm) {
            document.getElementById(componentForm[index]).value = '';
            document.getElementById(componentForm[index]).disabled = false;
        }

        var country, province, city, postalCode;
        var street = "";
        // Get each component of the address from the place details
        // and fill the corresponding field on the form.
        for (var i = 0; i < place.address_components.length; i++) {
            //console.log(place.address_components[i].types[0] + ": long name(" + place.address_components[i]["long_name"] + ") short name: (" + place.address_components[i]["short_name"] + ")");

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
        // if (country){
        // //  select country
        // }
        if (province) {
            // document.getElementById("checkout_shipping_address_province").value = province;
            if (province == "Québec") {
                province = "Quebec";
            }
            selectItemByValue(document.getElementById("checkout_shipping_address_province"), province);
        }
        // validate input address length
        validateMaxLength();
        // force lost focus
        document.activeElement.blur();
    }

    // load google address autocomplete js
    var googleMapEventListenerScript = document.createElement('script');
    googleMapEventListenerScript.async = true;
    googleMapEventListenerScript.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAwO_kj1b7YdHj_Gp567LepbrRGW4s-n4s&signed_in=true&libraries=places&callback=initAutocomplete&language="+getCountryLanguageCode();
    document.getElementsByTagName('head')[0].appendChild(googleMapEventListenerScript);


    function run(){
        var btnSubmit = document.getElementsByName("button")[0];
        addClickEvent(btnSubmit, function(e){
            validatePobox(e);
        })
    }

    try{
        run();
    }
    catch(err){

    }
})(window);

