(function(window) {

    function isCheckoutPage() {
        if (Shopify.Checkout.step) {
            if (Shopify.Checkout.step == "contact_information") {
                return true;
            }
        }
        return false;
    }
    store = {
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
    getStore = function() {
        return getStoreByUrl(window.location.href);
    }
    getCountryCode = function() {
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
    function removeIfExists(element) {
        if (element) {
            element.parentNode.removeChild(element);
        }
    }



    function addressAutocomplete() {
        componentForm = [
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
        if (getStore() == store.Spain) {
            delete componentFormMapping.administrative_area_level_1;
            componentFormMapping["administrative_area_level_2"] = 'long_name';
        }
        if (getStore() == store.Italy) {
            delete componentFormMapping.administrative_area_level_1;
            componentFormMapping["administrative_area_level_3"] = 'long_name';
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
        fillInAddress = function() {
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
            var phoneNumber = document.getElementById('checkout_shipping_address_phone');
            if (phoneNumber) {
                phoneNumber.focus();
            } else {
                document.activeElement.blur();
            }
        }
        var googleMapEventListenerScript = document.createElement('script');
        googleMapEventListenerScript.async = true;
        googleMapEventListenerScript.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAwO_kj1b7YdHj_Gp567LepbrRGW4s-n4s&signed_in=true&libraries=places&callback=initAutocomplete&language=" + getCountryLanguageCode();
        document.getElementsByTagName('head')[0].appendChild(googleMapEventListenerScript);
    }
    var Max_Length = 35;

    function validateMaxLength(e) {
        removeIfExists(document.getElementById("maxLength"));
        var currentValue = document.getElementById("checkout_shipping_address_address1").value;
        var currentLength = currentValue.length;

        if (currentLength == Max_Length) {
            address1 = document.getElementById("checkout_shipping_address_address1");
            address1.parentNode.setAttribute("class", "field__input-wrapper field field--required field--error");
            var maxLengthMessage = document.createElement("p");
            maxLengthMessage.setAttribute("id", "maxLength");
            maxLengthMessage.setAttribute("class", "field__message field__message--error");
            maxLengthMessage.setAttribute("style", "display:block");
            var textNode = document.createTextNode(getMaxLengthReachMessage());
            maxLengthMessage.appendChild(textNode);
            address1.parentNode.appendChild(maxLengthMessage);
        }
    };
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
    window.isPobox = function(address) {
        return /p\.?o(\.|\-|st|(st\s*office))?\s*box\s*\-?#?\s*\d*/i.test(address);
    }

    function validatePobox(e) {
        if (getStore() != store.Canada_En && getStore() != store.Canada_Fr) {
            removeIfExists(document.getElementById("poboxMessage"));
            ClearValidationResult(validationResultKey.Address1);
            var address1 = document.getElementById("checkout_shipping_address_address1");
            if (address1) {
                if (isPobox(address1.value)) {
                    address1.parentNode.setAttribute("class", "field__input-wrapper field field--required field--error");
                    address1.setAttribute("class", "boxshadow field--error field__input");
                    var poboxMessage = document.createElement("p");
                    poboxMessage.setAttribute("id", "poboxMessage");
                    poboxMessage.setAttribute("class", "field__message field__message--error");
                    poboxMessage.setAttribute("style", "display:block");
                    var textNode = document.createTextNode(getPoboxMessage());
                    poboxMessage.appendChild(textNode);
                    address1.parentNode.appendChild(poboxMessage);
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
    function validatePhoneNumber(e) {
        if (getStore() == store.Canada_En || getStore() == store.Canada_Fr || getStore() == store.UnitedStates) {
            var phoneNumber = document.getElementById("checkout_shipping_address_phone");
            var phoneNumberMessage = document.getElementById("phoneNumberMessage");
            if (phoneNumberMessage) {
                phoneNumberMessage.parentNode.removeChild(phoneNumberMessage);
            }
            ClearValidationResult(validationResultKey.PhoneNumber);
            var phoneNumberArray = phoneNumber.value.split("");
            var numberCount = 0;
            for (var i = 0; i < phoneNumberArray.length; i++) {
                var val = phoneNumberArray[i];
                if (!isNaN(val)) {
                    numberCount++;
                }
            }
            if (numberCount < 10) {
                phoneNumber.parentNode.setAttribute("class", "field field--required field--error");
                phoneNumberMessage = document.createElement("p");
                phoneNumberMessage.setAttribute("id", "phoneNumberMessage");
                phoneNumberMessage.setAttribute("class", "field__message field__message--error");
                phoneNumberMessage.setAttribute("style", "display:block");
                var textNode = document.createTextNode(getPhoneNumberTooShortMessage());
                phoneNumberMessage.appendChild(textNode);
                phoneNumber.parentNode.appendChild(phoneNumberMessage);
                AddValidationResult(validationResultKey.PhoneNumber, "wrong phone number");
            }
        }
    }
    function getPhoneNumberTooShortMessage() {
        return "Phone number must be at least 10 numeric characters";
    }
    validationResult = {};
    validationResultKey = {
        PhoneNumber: "phoneNumber",
        Address1: "address1"
    }
    AddValidationResult = function(key, value) {
        window.validationResult[key] = value;
    }
    ClearValidationResult = function(key) {
        delete window.validationResult[key];
    }
    CheckValidation = function(e) {
        for (var key in window.validationResult) {
            if (validationResult.hasOwnProperty(key)) {
                e.preventDefault();
            }
        }
    }

    function run() {
        addressAutocomplete();
        var fields = "checkout_shipping_address_first_name checkout_shipping_address_last_name checkout_shipping_address_address1 checkout_shipping_address_address2 checkout_shipping_address_city checkout_shipping_address_zip checkout_shipping_address_phone checkout_billing_address_first_name checkout_billing_address_last_name checkout_billing_address_address1 checkout_billing_address_address2 checkout_billing_address_city checkout_billing_address_zip checkout_billing_address_phone".split(" ");
        for (i = 0; i < fields.length; i++) {
            if (document.getElementById(fields[i])) document.getElementById(fields[i]).maxLength = Max_Length;
        }
        var address1 = document.getElementById("checkout_shipping_address_address1");
        if (address1) {
            address1.maxLength = Max_Length;
            addOnKeyupEvent(address1, validateMaxLength);
            addOnKeyupEvent(address1, validatePobox);
        }

        var btnSubmit = document.getElementsByName("button")[0];
        addClickEvent(btnSubmit, function(e) {
            CheckValidation(e);
        });
        addClickEvent(btnSubmit, function(e) {
            validatePhoneNumber(e);
        });
       addClickEvent(btnSubmit, function(e) {
            validatePobox(e);
        })
    }
    try {
        if (isCheckoutPage()){
            run();
        }        
    } catch (err) {}
})(window);