﻿$(document).ready(function () {
    var User = {
        lat: 40.4287711,
        lng: 49.2481203
    }
    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            console.log("Geolocation is not supported by this browser.");
        }
    }

    function showPosition(position) {
        map.setCenter({ lat: position.coords.latitude, lng: position.coords.longitude });
    }

    getLocation();

    var markerAdd = false;

    map = new GMaps({
        div: '#map',
        zoom: 14,
        lat: User.lat,
        lng: User.lng,
       
      
    });

    $(".mapitem").each(function (index) {
        map.addMarker({
            lat: $(this).data("lat"),
            lng: $(this).data("lng"),
            title: $(this).data("name"),
            draggable: false,

        });
    });

    function showPosition(position) {
        map.setCenter({ lat: $(".mapitem:first").data("lat"), lng: $(".mapitem:first").data("lng") });
    }

});



$(".card").click(function () {
    a = $(this);
    $(".mapitem").each(function (index) {
        if ($(this).data("name") == a.data("name")) {
            map.setCenter({ lat: $(this).data("lat"), lng: $(this).data("lng") });
        }

    });
});