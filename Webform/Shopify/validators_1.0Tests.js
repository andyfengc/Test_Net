
// test is pobox
test("is pobox", function(){
	ok(isPobox("pobox"), "this is a pobox address");
	ok(isPobox("po.box"), "this is a pobox address");
	ok(isPobox("po.box"), "this is a pobox address");
	ok(isPobox("p.o.box"), "this is a pobox address");
	ok(isPobox("PO box"), "this is a pobox address");
	ok(isPobox("P.O. box"), "this is a pobox address");
})

test("identify current store should succeed", function(){
	equal(getStoreByUrl("https://us.kobobooks.com/"), store.UnitedStates, "this is US store");
	equal(getStoreByUrl("https://ca.kobobooks.com/"), store.Canada_En, "this is Canada store");
	equal(getStoreByUrl("https://au.kobobooks.com"), store.Australia, "this is Australia store");
})


// test portugal postal code
test("is valid portugal postal code", function(){
	ok(isValidPortugalPostalCode("4750"), "this is a valid portugal postal code");
	ok(isValidPortugalPostalCode("4750-828"), "this is a valid portugal postal code");
	ok(isValidPortugalPostalCode("3800-149 Aveiro"), "this is a valid portugal postal code");
	ok(isValidPortugalPostalCode("4455-111 Paradela VNB"), "this is a valid portugal postal code");
	ok(isValidPortugalPostalCode("3030 - 108"), "this is a valid portugal postal code");
})

test("is not valid portugal postal code", function(){
	notOk(isValidPortugalPostalCode("4OOO-217"), "this is not a valid portugal postal code");
	notOk(isValidPortugalPostalCode("12345"), "this is not a valid portugal postal code");
	notOk(isValidPortugalPostalCode("1990601"), "this is not a valid portugal postal code");
	notOk(isValidPortugalPostalCode("3400 691"), "this is not a valid portugal postal code");
	notOk(isValidPortugalPostalCode("47oo"), "this is not a valid portugal postal code");
})

// test canadian postal code
test("is valid canadian postal code", function(){
	ok(isValidCanadianPostalCode("M6K 1a7"), "this is a valid canadian postal code");
	ok(isValidCanadianPostalCode("M6K-3N7"), "this is a valid canadian postal code");
	ok(isValidCanadianPostalCode("m6k3R6"), "this is a valid canadian postal code");
	ok(isValidCanadianPostalCode("M6S 2S7"), "this is a valid canadian postal code");
	ok(isValidCanadianPostalCode("T2E 7V6"), "this is a valid canadian postal code");
})

test("is not valid canadian postal code", function(){
	notOk(isValidCanadianPostalCode("4OOO-217"), "this is not a valid canadian postal code");
	notOk(isValidCanadianPostalCode("12345"), "this is not a valid canadian postal code");
	notOk(isValidCanadianPostalCode("1990601"), "this is not a valid canadian postal code");
	notOk(isValidCanadianPostalCode("3400 691"), "this is not a valid canadian postal code");
	notOk(isValidCanadianPostalCode("47oo"), "this is not a valid canadian postal code");
})

// test us postal code
test("is valid US postal code", function(){
	ok(isValidUsPostalCode("94105-0011"), "this is a valid US postal code");
	ok(isValidUsPostalCode("94105"), "this is a valid US postal code");
	ok(isValidUsPostalCode("10039"), "this is a valid US postal code");
	ok(isValidUsPostalCode("10001 -2381"), "this is a valid US postal code");
	ok(isValidUsPostalCode("29839 - 1234"), "this is a valid US postal code");
})

test("is not valid US postal code", function(){
	notOk(isValidCanadianPostalCode("4OOO-217"), "this is not a valid US postal code");
	notOk(isValidCanadianPostalCode("12222-12345"), "this is not a valid US postal code");
	notOk(isValidCanadianPostalCode("1002 - 1283"), "this is not a valid US postal code");
	notOk(isValidCanadianPostalCode("3400 691"), "this is not a valid US postal code");
	notOk(isValidCanadianPostalCode("1OOO1"), "this is not a valid US postal code");
})

