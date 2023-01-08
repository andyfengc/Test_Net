(function(window) {

    function isCheckoutPage() {
        if (Shopify.Checkout.step) {
            if (Shopify.Checkout.step == "contact_information") {
                return true;
            }
        }
        return false;
    }

    // global variables
    store = {
        // NA
        Canada_En: {
            url: "https://ca.kobobooks.com",
            pattern: /ca\.kobo/i
        },
        Canada_Fr: {
            url: "https://ca-fr.kobobooks.com",
            pattern: /ca-fr\.kobo/i
        },
        UnitedStates: {
            url: "https://us.kobobooks.com",
            pattern: /us\.kobo/i
        }
        // EU
        ,
        UnitedKingdom: {
            url: "https://uk.kobobooks.com",
            pattern: /uk\.kobo/i
        },
        Italy: {
            url: "https://it.kobobooks.com",
            pattern: /it\.kobo/i
        },
        Spain: {
            url: "https://es.kobobooks.com",
            pattern: /es\.kobo/i
        },
        Germany: {
            url: "https://de.kobobooks.com",
            pattern: /de\.kobo/i
        },
        France: {
            url: "https://fr.kobobooks.com",
            pattern: /fr\.kobo/i
        },
        Portugal: {
            url: "https://pt.kobobooks.com",
            pattern: /pt\.kobo/i
        }
        // oceania
        ,
        Australia: {
            url: "https://au.kobobooks.com",
            pattern: /au\.kobo/i
        },
        NewZealand: {
            url: "https://nz.kobobooks.com",
            pattern: /nz\.kobo/i
        },
        RkbTest: {
            url: "https://checkout.shopify",
            pattern: /checkout\.shopify/i
        },
        Localhost: {
            url: "http://localhost",
            pattern: /localhost:/i
        }
    }

    // check store
    window.getStoreByUrl = function(url) {
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

    // address auto complete
    // get current store
    getStore = function() {
        return getStoreByUrl("https://us.kobobooks.com/");
        //return getStoreByUrl(window.location.href);
    }

    // get country code
    getCountryCode = function() {
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
            case store.UnitedStates:
            return "US";
            case store.Germany:
            return "DE";
            case store.Spain:
            return "ES";
            case store.Italy:
            return "IT";
            case store.Portugal:
            return "PT";
            case store.France:
            return "FR";
            case store.Australia:
            return "AU";
            case store.NewZealand:
            return "NZ";
            case store.UnitedKingdom:
            return "GB";
            case store.Canada_En:
            case store.Canada_Fr:
            default:
            return "CA";
        }
    }

    // get country language code
    window.getCountryLanguageCode = function() {
        switch (getStore()) {
            case store.Canada_Fr:
            case store.France:
            return "fr";
            case store.Germany:
            return "de";
            case store.Spain:
            return "es";
            case store.Italy:
            return "it";
            case store.Portugal:
            return "pt-PT";
            case store.Australia:
            case store.NewZealand:
            return "en-AU";
            case store.UnitedKingdom:
            return "en-GB";
            case store.Canada_En:
            case store.UnitedStates:
            default:
            return "en";
        }
    }

    addClickEvent = function(element, func) {
        var oldClick = element.onclick;
        if (typeof oldClick != "function") {
            element.onclick = func;
        } else {
            element.onclick = function(e) {
                func(e);
                oldClick(e);
            }
        }
    }

    addOnKeyupEvent = function(element, func) {
        var oldKeyup = element.onkeyup;
        if (typeof oldKeyup != "function") {
            element.onkeyup = func;
        } else {
            element.onkeyup = function(e) {
                func(e);
                oldKeyup(e);
            }
        }
    }

    function selectItemByValue(select, value) {
        for (var i = 0; i < select.options.length; i++) {
            if (select.options[i].value === value) {
                select.selectedIndex = i;
                break;
            }
        }
    }

    // remove an element
    function removeIfExists(element) {
        if (element) {
            element.parentNode.removeChild(element);
        }
    }


    function addressAutocomplete() {
        // address control ids
        componentForm = [
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
        if (getStore() == store.Spain) {
            delete componentFormMapping.administrative_area_level_1;
            componentFormMapping["administrative_area_level_2"] = 'long_name';
        }
        if (getStore() == store.Italy) {
            delete componentFormMapping.administrative_area_level_1;
            componentFormMapping["administrative_area_level_3"] = 'long_name';
        }



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
            if (address1) {
                google.maps.event.addDomListener(address1, 'keydown', function(e) {
                    if (e.keyCode == 13) {
                        e.preventDefault();
                    }
                });
                autocomplete = new google.maps.places.Autocomplete((document.getElementById('checkout_shipping_address_address1')), options);
                autocomplete.addListener('place_changed', fillInAddress);
            }
        }

        // process address autocomplete dropdown select event
        fillInAddress = function() {
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

            // focus the next element
            var phoneNumber = document.getElementById('checkout_shipping_address_phone');
            if (phoneNumber) {
                phoneNumber.focus();
            } else {
                // force lost focus
                document.activeElement.blur();
            }
        }

        // load google address autocomplete js
        var googleMapEventListenerScript = document.createElement('script');
        googleMapEventListenerScript.async = true;
        googleMapEventListenerScript.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAwO_kj1b7YdHj_Gp567LepbrRGW4s-n4s&signed_in=true&libraries=places&callback=initAutocomplete&language=" + getCountryLanguageCode();
        document.getElementsByTagName('head')[0].appendChild(googleMapEventListenerScript);
    }

    //
    // add validation error message under textbox
    function addErrorMessage(txtElement, message, messageElementId){
        if (txtElement){
            txtElement.parentNode.setAttribute("class", "field__input-wrapper field field--required field--error");
            txtElement.setAttribute("class", "boxshadow field--error field__input");
            var messageElement = document.createElement("p");
            messageElement.setAttribute("id", messageElementId);
            messageElement.setAttribute("class", "field__message field__message--error");
            messageElement.setAttribute("style", "display:block");
            var textNode = document.createTextNode(message);
            messageElement.appendChild(textNode);
            txtElement.parentNode.appendChild(messageElement);
        }        
    }

    // validate maxlength of address1
    var Max_Length = 35;

    function validateMaxLength(e) {
        // remove maxlength message if exists
        removeIfExists(document.getElementById("maxLength"));
        // validate
        var currentValue = document.getElementById("checkout_shipping_address_address1").value;
        var currentLength = currentValue.length;

        if (currentLength == Max_Length) {
            // display max length over limitation message
            address1 = document.getElementById("checkout_shipping_address_address1");
            addErrorMessage(address1, getMaxLengthReachMessage(), "maxLength");
        }
    };

    // the max length of 35 characters is reached
    function getMaxLengthReachMessage() {
        switch (getStore()) {
            case store.Canada_Fr:
            case store.France:
            return "la longueur maximum de " + Max_Length + " caractères est atteint";
            case store.Germany:
            return "die maximale Länge " + Max_Length + " Zeichen erreicht ist";
            case store.Spain:
            return "la longitud máxima de " + Max_Length + " caracteres se alcanza";
            case store.Italy:
            return "la lunghezza massima di " + Max_Length + " personaggi è raggiunto";
            case store.Portugal:
            return "o comprimento máximo de " + Max_Length + " caracteres é atingido";
            case store.Australia:
            return "the max length of " + Max_Length + " characters is reached";
            case store.NewZealand:
            return "the max length of " + Max_Length + " characters is reached";
            case store.UnitedKingdom:
            return "the max length of " + Max_Length + " characters is reached";
            case store.Canada_En:
            case store.UnitedStates:
            default:
            return "the max length of " + Max_Length + " characters is reached";
        }
    }

    // pobox validator
    window.isPobox = function(address) {
        return /p\.?o(\.|\-|st|(st\s*office))?\s*box\s*\-?#?\s*\d*/i.test(address);
    }

    function validatePobox(e) {
        if (getStore() != store.Canada_En && getStore() != store.Canada_Fr) {
            // remove pobox message if exists
            removeIfExists(document.getElementById("poboxMessage"));
            // clear validation result if has any
            ClearValidationResult(validationResultKey.Address1);
            // validate
            var address1 = document.getElementById("checkout_shipping_address_address1");
            if (address1) {
                if (isPobox(address1.value)) {
                    addErrorMessage(address1,getPoboxMessage(), "poboxMessage")
                    //e.preventDefault();
                    AddValidationResult(validationResultKey.Address1, "wrong pobox number");
                }
            }
        }
    }

    function getPoboxMessage(){
        switch (getStore()) {
            case store.France:
            return "Kobo ne livrons pas à des boîtes postales et nécessitera une adresse pour la livraison";
            case store.Germany:
            return "Kobo nicht an Postfächer versenden und eine Adresse für die Lieferung erforderlich";
            case store.Spain:
            return "Kobo no realiza envíos a apartados postales y requerirá una dirección para la entrega";
            case store.Italy:
            return "Kobo non spedisce a caselle postali e richiederà un indirizzo per la consegna";
            case store.Portugal:
            return "O Kobo não enviar para caixas postais e vai exigir um endereço para entrega";
            case store.Australia:
            case store.NewZealand:
            case store.UnitedKingdom:
            case store.UnitedStates:
            default:
            return "Kobo does not ship to PO Boxes and will require a street address for delivery";
        }
    }

    //
    // phone number validator
    function isNumber(character){
        return /[0-9]/.test(character);
    }
    function validatePhoneNumber(e) {
        if (getStore() == store.Canada_En || getStore() == store.Canada_Fr || getStore() == store.UnitedStates) {
            var phoneNumber = document.getElementById("checkout_shipping_address_phone");
            // remove alert message if exists
            var phoneNumberMessage = document.getElementById("phoneNumberMessage");
            if (phoneNumberMessage) {
                phoneNumberMessage.parentNode.removeChild(phoneNumberMessage);
            }
            // clear validation result if has any
            ClearValidationResult(validationResultKey.PhoneNumber);
            // validate now
            var phoneNumberArray = phoneNumber.value.split("");
            var numberCount = 0;
            for (var i = 0; i < phoneNumberArray.length; i++) {
                var val = phoneNumberArray[i];
                if (isNumber(val)) {
                    numberCount++;
                }
            }
            if (numberCount < 10) {
                // add border
                phoneNumber.parentNode.setAttribute("class", "field field--required field--error");
                // add message
                addErrorMessage(phoneNumber, getPhoneNumberTooShortMessage(), "phoneNumberMessage")

                AddValidationResult(validationResultKey.PhoneNumber, "wrong phone number");
                //e.preventDefault();
            }
        }
    }

    // phone number too short messsage
    function getPhoneNumberTooShortMessage() {
        return "Phone number must be at least 10 numeric characters";
    }

    // postal code validator
    window.isValidPortugalPostalCode = function(address) {
        return /^\d{4}\b(\s*\-\s*\d{3})?(\s*\D*)?(\s*\D{3})?$/i.test(address);
    }

    window.isValidCanadianPostalCode = function(address) {
        return /^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1}[\s|-]*\d{1}[A-Z]{1}\d{1}$/i.test(address);
    }

    window.isValidUsPostalCode = function(address) {
        return /^\d{5}(\s*\-\s*\d{4})?$/i.test(address);
    }

    function validatePostalCode(e) {
        var postalCodeMessageId = "postalCodeMessage";
        if (getStore() == store.Portugal) {
            // remove postal code message if exists
            removeIfExists(document.getElementById(postalCodeMessageId));
            // clear validation result if has any
            ClearValidationResult(validationResultKey.PostalCode);
            // validate
            var postalCode = document.getElementById("checkout_shipping_address_zip");
            if (postalCode) {
                if (postalCode.value && !isValidPortugalPostalCode(postalCode.value)) {
                    addErrorMessage(postalCode, getInvalidPostalCodeMessage(), postalCodeMessageId);
                    //e.preventDefault();
                    AddValidationResult(validationResultKey.PostalCode, "wrong postal code");
                }
            }
        }
        if (getStore() == store.Canada_En || getStore() == store.Canada_Fr){
            // remove postal code message if exists
            removeIfExists(document.getElementById(postalCodeMessageId));
            // clear validation result if has any
            ClearValidationResult(validationResultKey.PostalCode);
            // validate
            var postalCode = document.getElementById("checkout_shipping_address_zip");
            if (postalCode) {
                if (postalCode.value && !isValidCanadianPostalCode(postalCode.value)) {
                    addErrorMessage(postalCode, getInvalidPostalCodeMessage(), postalCodeMessageId);
                    //e.preventDefault();
                    AddValidationResult(validationResultKey.PostalCode, "wrong postal code");
                }
            }
        }
        if (getStore() == store.UnitedStates){
            // remove postal code message if exists
            removeIfExists(document.getElementById(postalCodeMessageId));
            // clear validation result if has any
            ClearValidationResult(validationResultKey.PostalCode);
            // validate
            var postalCode = document.getElementById("checkout_shipping_address_zip");
            if (postalCode) {
                if (postalCode.value && !isValidUsPostalCode(postalCode.value)) {
                    addErrorMessage(postalCode, getInvalidPostalCodeMessage(), postalCodeMessageId);
                    //e.preventDefault();
                    AddValidationResult(validationResultKey.PostalCode, "wrong postal code");
                }
            }
        }
    }


    function getInvalidPostalCodeMessage(){
        switch (getStore()) {
            case store.Portugal:
                return "Por favor, indique o código postal válido Português";
            case store.Canada_Fr:
                return "S'il vous plaît entrez le code postal valide";
            case store.UnitedStates:
                return "Please enter valid zip code";
            case store.Canada_En:
            default:
                return "Please enter valid postal code";
        }
    }


    // define a global array variable to save validation results
    validationResult = {};
    validationResultKey = {
        PhoneNumber: "phoneNumber",
        Address1: "address1",
        PostalCode: "postalCode"
    }

    // add validation result
    AddValidationResult = function(key, value) {
        window.validationResult[key] = value;
    }

    // clear validation result
    ClearValidationResult = function(key) {
        delete window.validationResult[key];
    }

    // stop submit if has any validation results, should be called after all other validations
    CheckValidation = function(e) {
        for (var key in window.validationResult) {
            if (validationResult.hasOwnProperty(key)) {
                e.preventDefault();
            }
        }
    }

    function run() {
        // launch google address auto complete
        addressAutocomplete();

        // add 35 character limits for all fields
        var fields = "checkout_shipping_address_first_name checkout_shipping_address_last_name checkout_shipping_address_address1 checkout_shipping_address_address2 checkout_shipping_address_city checkout_shipping_address_zip checkout_shipping_address_phone checkout_billing_address_first_name checkout_billing_address_last_name checkout_billing_address_address1 checkout_billing_address_address2 checkout_billing_address_city checkout_billing_address_zip checkout_billing_address_phone".split(" ");
        for (i = 0; i < fields.length; i++) {
            if (document.getElementById(fields[i])) document.getElementById(fields[i]).maxLength = Max_Length;
        }

        // add address maxlength message label
        var address1 = document.getElementById("checkout_shipping_address_address1");
        if (address1) {
            // set max size
            address1.maxLength = Max_Length;
            // register address maxlength events
            addOnKeyupEvent(address1, validateMaxLength);

            // add pobox verification for us store
            addOnKeyupEvent(address1, validatePobox);
        }

        var btnSubmit = document.getElementsByName("button")[0];
        // add validation summary function
        addClickEvent(btnSubmit, function(e) {
            CheckValidation(e);
        });

        // add phone number validator
        addClickEvent(btnSubmit, function(e) {
            validatePhoneNumber(e);
        });

         // add pobox validator
         addClickEvent(btnSubmit, function(e) {
            validatePobox(e);
        })

       // add postal code validator
       addClickEvent(btnSubmit, function(e){
        validatePostalCode(e);
    });
   }

    //
    try {
        if (isCheckoutPage()){
            run();
        }        
    } catch (err) {}
})(window);