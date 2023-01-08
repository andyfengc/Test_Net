var app = angular.module("myApp", ["kendo.directives"]);

// define empty controller
app.controller("myController", function ($scope) {
});

// define calendar controller
app.controller("ctrl2", function($scope) {
    $scope.monthPickerConfig = {
        start: "year",
        depth: "year",
        format: "MMMM yyyy"
    };
});
