(function () {
    function validatePhoneNumber(e) {
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
            if (!isNaN(val)) {
                numberCount++;
            }
        }
        if (numberCount < 10) {
            // add border
            phoneNumber.parentNode.setAttribute("class", "field field--required field--error");
            // display alert message
            var message = "Phone number must be at least 10 numeric characters";
            // add message
            phoneNumberMessage = document.createElement("p");
            phoneNumberMessage.setAttribute("id", "phoneNumberMessage");
            phoneNumberMessage.setAttribute("class", "field__message field__message--error");
            phoneNumberMessage.setAttribute("style", "display:block");
            var textNode = document.createTextNode(message);
            phoneNumberMessage.appendChild(textNode);
            phoneNumber.parentNode.appendChild(phoneNumberMessage);
            AddValidationResult(validationResultKey.PhoneNumber, "wrong phone number");
            //e.preventDefault();
        }
    }
    //
    function run() {
        var btnSubmit = document.getElementsByName("button")[0];
        addClickEvent(btnSubmit, function(e){
            validatePhoneNumber(e);
        })
                //btnSubmit.onclick = function (e) {
        //    validatePhoneNumber(e);
        //}
    }
    //
    try {
        run();
    } catch (err) { }
})();





