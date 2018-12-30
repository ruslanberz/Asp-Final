$(document).ready(function () {
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
        click: function (e) {
            if (!markerAdd) {
                UpdateLocation(e.latLng.lat(), e.latLng.lng());
                map.addMarker({
                    lat: e.latLng.lat(),
                    lng: e.latLng.lng(),
                    title: 'Lima',
                    draggable: true,
                    dragend: function (e) {
                        UpdateLocation(e.latLng.lat(), e.latLng.lng());
                    }
                });
                markerAdd = true;
            }
        }
    });

    function UpdateLocation(lat, lng) {
        $("input[name='lat']").val(lat);
        $("input[name='lng']").val(lng);
    }

});