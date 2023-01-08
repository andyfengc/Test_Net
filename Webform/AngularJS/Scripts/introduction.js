var app = angular.module("myModule", []);

app.controller("myController", function($scope) {
    var employers = [
        { name: "John", age: 31, gender: "male", salary: 5000, new: false },
        { name: "Kathy", age: 22, gender: "female", salary: 2500, new: true },
        { name: "Andy", age: 30, gender: "male", salary: 4900, new: false },
        { name: "Betty", age: 29, gender: "female", salary: 3000, new: false },
        { name: "Kitty", age: 26, gender: "female", salary: 2800, new: true }
    ];
    $scope.employers = employers;

})