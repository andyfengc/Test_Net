// test isNumber()
test("if a number", function(){
	ok(isNumber(5), "5 is a number")
})

test("if a number", function(){
	var a = 92;
	ok(isNumber(a), a + " is a number")
})

// test square() function
test("square exists", function(){
	ok(square, "square exists");
})

test("square is function", function(){
	ok(typeof square === 'function', "square is a function");
})

test("square returns", function(){
	for (var i = 0; i < 12; i++){
		equal(square(i), i * i, i + " * " + i + " = " + square(i));
	}
})