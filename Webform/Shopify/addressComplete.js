var componentForm = [
"checkout_shipping_address_city",
"checkout_shipping_address_zip",
//"checkout_shipping_address_country",
"checkout_shipping_address_province"];

var placeSearch, autocomplete;

var componentFormMapping = {
  street_number: 'short_name',
  route: 'long_name',
  locality: 'long_name',
  //administrative_area_level_1: 'short_name',
  administrative_area_level_1: 'long_name',
  country: 'long_name',
  postal_code: 'short_name'
};

// common
function isPrintableKey(e){
  var keycode = e.keyCode;
  var valid = 
        (keycode > 47 && keycode < 58)   || // number keys
        keycode == 32 || keycode == 13   || // spacebar & return key(s) (if you want to allow carriage returns)
        (keycode > 64 && keycode < 91)   || // letter keys
        (keycode > 95 && keycode < 112)  || // numpad keys
        (keycode > 185 && keycode < 193) || // ;=,-./` (in order)
        (keycode > 218 && keycode < 223);   // [\]' (in order)
        return valid;
      }
      function isNonPrintableKey(e){
        return !isPrintableKey(e);
      }
      function isCombinationKeyPressed(e){
        return e.ctrlKey || e.altKey || e.shiftKey || e.metaKey;
      }

      function selectItemByValue(select, value){

        for(var i=0; i < select.options.length; i++)
        {
          if(select.options[i].value === value) {
            select.selectedIndex = i;
            break;
          }
        }
      }
// maxlength
var Max_Length = 30;
var maxLengthMessage = " characters remaining ("+Max_Length+" maximum)";

function validateMaxLength(e){
  var currentValue = document.getElementById("checkout_shipping_address_address1").value;
  var currentLength;

  if (e){// is a event
    if (e.type == "keypress" || e.type == "keydown" ){
      if (e.keyCode == '8'){
        if (currentValue.length == 0){
          currentLength = currentValue.length;
        }
        else{
          currentLength = currentValue.length-1;
        }      
      }
      else if (isNonPrintableKey(e) || isCombinationKeyPressed(e)){
        currentLength = currentValue.length;
      }    
    else{// printable key
      currentLength = currentValue.length+1;
    }
  }
  else {
    currentLength = currentValue.length;
  }
}
else{
  currentLength = currentValue.length;
}

var maxLength = document.getElementById("maxLength");
if (currentLength > Max_Length){
  address1 = document.getElementById("checkout_shipping_address_address1");
  address1.parentNode.className = "field field--required field--error field--focus";
  //maxLength.innerHTML = "the max length of <b>"+Max_Length+"</b> characters is reached, you typed in  <b>" + currentLength + "</b> characters";
  maxLength.innerHTML = "the max length of <b>"+Max_Length+"</b> characters is reached";
}
else{
  address1 = document.getElementById("checkout_shipping_address_address1");
  address1.parentNode.className = "field field--required";
  var currentLeftLength = Max_Length - currentLength;
  maxLength.innerHTML = currentLeftLength+ maxLengthMessage;
}
};
var address1 = document.getElementById("checkout_shipping_address_address1");
address1.parentNode.innerHTML = address1.parentNode.innerHTML + "<p id=\"maxLength\" class=\"field__error-message warning\"></p>";
var maxLength = document.getElementById("maxLength");
address1 = document.getElementById("checkout_shipping_address_address1");
// set max size
address1.maxLength = Max_Length;

maxLength.style.display = "block";
var currentLeftLength = Max_Length;
maxLength.innerHTML = currentLeftLength+ maxLengthMessage;

address1.onkeydown = validateMaxLength;
address1.onblur = validateMaxLength;
// address1.onfocus = validateMaxLength;
// address1.onfocus = 
// address1.onmouseover = validateMaxLength;
// addAddressFocusOut(validateMaxLength);
// address1.onkeyup = validateMaxLength;

window.getCountryCode=function(){
  // set default country
  document.getElementById("checkout_shipping_address_country").selectedIndex = 229;

  var select = document.getElementById("checkout_shipping_address_country");
  var country = select.options[select.selectedIndex].value;
  switch (country){
    case "Canada": return "CA";
    case "United States": return "US";
    case "United Kingdom": return "GB";
    case "Italy": return "IT";
    case "France": return "FR";
    case "Spain": return "ES";
    case "Portugal": return "PT";
    case "Germany": return "DE";
  }
  return "";
}

window.initAutocomplete = function() {
  var options = {
    types:['address'],
    componentRestrictions: { country: getCountryCode() },
  };
  if (options.componentRestrictions.country == ""){
    delete options.componentRestrictions;
  }
  autocomplete = new google.maps.places.Autocomplete((document.getElementById('checkout_shipping_address_address1')),options);
  autocomplete.addListener('place_changed', fillInAddress);
}

function fillInAddress() {
  // Get the place details from the autocomplete object.
  var place = autocomplete.getPlace();

  for (var index in componentForm){
    document.getElementById(componentForm[index]).value = '';
    document.getElementById(componentForm[index]).disabled = false;
  }

  var country, province, city, postalCode;
  var street = "";
  // Get each component of the address from the place details
  // and fill the corresponding field on the form.
  for (var i = 0; i < place.address_components.length; i++) {
    //console.log(place.address_components[i].types[0]);
    var addressType = place.address_components[i].types[0];

    if (componentFormMapping[addressType]) {
      var val = place.address_components[i][componentFormMapping[addressType]];
      if(addressType == "street_number"){
        street+=val;
      }
      if(addressType == "route"){
        if (street != ""){
          street+=" " + val;
        }
        else{
          street +=val;
        }
      }
      if (addressType == "locality"){
       city = val;
     }
     else if (addressType == "postal_code"){
       postalCode = val;
     }
     else if (addressType == "country"){
       country = val;
     }
     else{
       province = val;
     }
   }
 }
 if (street){
  document.getElementById("checkout_shipping_address_address1").value = street;
}
if (city){
  document.getElementById("checkout_shipping_address_city").value = city;
}
if (postalCode){
  document.getElementById("checkout_shipping_address_zip").value = postalCode;
}
// if (country){
// //  select country
// }
if (province){
  // document.getElementById("checkout_shipping_address_province").value = province;
  if (province == "Québec"){
    province = "Quebec";
  }
  selectItemByValue(document.getElementById("checkout_shipping_address_province"), province);
}
// validate input address length
validateMaxLength();
}


var googleMapEventListenerScript = document.createElement('script');
googleMapEventListenerScript.async = true;
googleMapEventListenerScript.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAwO_kj1b7YdHj_Gp567LepbrRGW4s-n4s&signed_in=true&libraries=places&callback=initAutocomplete";
document.getElementsByTagName('head')[0].appendChild(googleMapEventListenerScript);

// var googleMapEventListenerScript = document.createElement('script');
// googleMapEventListenerScript.async = true;
// googleMapEventListenerScript.src = "https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js";
// document.getElementsByTagName('head')[0].appendChild(googleMapEventListenerScript);
