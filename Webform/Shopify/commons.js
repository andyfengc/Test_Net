// define global function
(function(window){
	window.store = {
        // NA
        Canada_En: { url: "https://ca.kobobooks.com", pattern: /ca\.kobo/i }
        ,Canada_Fr: { url: "https://ca-fr.kobobooks.com", pattern: /ca-fr\.kobo/i}
        ,UnitedStates: { url: "https://us.kobobooks.com", pattern: /us\.kobo/i }
        // EU
        ,UnitedKingdom: { url: "https://uk.kobobooks.com", pattern: /uk\.kobo/i }
        ,Italy: { url: "https://it.kobobooks.com", pattern: /it\.kobo/i }
        ,Spain: { url: "https://es.kobobooks.com", pattern: /es\.kobo/i }
        ,Germany: { url: "https://de.kobobooks.com", pattern: /de\.kobo/i }
        ,France: { url: "https://fr.kobobooks.com", pattern: /fr\.kobo/i }
        ,Portugal: { url: "https://pt.kobobooks.com", pattern: /pt\.kobo/i }
        // oceania
        ,Australia: { url: "https://au.kobobooks.com", pattern: /au\.kobo/i }
        ,NewZealand: { url: "https://nz.kobobooks.com", pattern: /nz\.kobo/i }
        ,RkbTest:{ url: "https://checkout.shopify", pattern: /checkout\.shopify/i }
        ,Localhost:{ url: "http://localhost", pattern: /localhost:/i }
    }

    // check store
    window.getStoreByUrl = function(url){
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

    window.isPobox = function(address) {
    	return /p\.?o(\.|\-|st|(st\s*office))?\s*box\s*\-?#?\s*\d*/i.test(address);
    }

    window.addClickEvent = function(element, func){
    	var oldClick = element.onclick;
    	if (typeof oldClick != "function"){
    		element.onclick = func;
    	}
    	else{
    		element.onclick = function(e){
    			func(e);
    			oldClick(e);
    		}
    	}
    }

    // define a global array variable to save validation results
    window.validationResult = {};
    window.validationResultKey = {
    	PhoneNumber: "phoneNumber",
    	Address1: "address1"
    }

    // add validation result
    window.AddValidationResult = function(key, value){
    	window.validationResult[key] = value;
    }

    window.ClearValidationResult = function(key){
    	delete window.validationResult[key];
    }

    //  stop submit if has any validation results, should be called after all other validations
    window.CheckValidation = function(e) {
    	for (var key in window.validationResult){
    		if (validationResult.hasOwnProperty(key)){
    			e.preventDefault();
    		}
    	}
    }

	var btnSubmit = document.getElementsByName("button")[0];
    addClickEvent(btnSubmit, function(e){
        CheckValidation(e);
    });



})(window);