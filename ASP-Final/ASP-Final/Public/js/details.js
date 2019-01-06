$(document).ready(function () {

    $(".my-rating").starRating({
        initialRating: 4,
        strokeColor: '#894A00',
        hoverColor:'#46cd38',
        ratedColor: '#ff4b00',
        strokeWidth: 10,
        starSize: 25,
        useGradient:true,
        callback: function (currentRating, $el) {
            $("#rating").attr('data-rating', currentRating);  
        }
    });
    $("#rate").submit(function (e) {
        e.preventDefault();
        var rating = Number ($("#rating").data("rating"))*2;
        var text = $('textarea#commentText').val();
        var id = Number($("#rate").data("id"))
        $.ajax({
            type: 'post',
            dataType: "json",
            url: "/place/publishcomment",
            data:
            {
                Comment: text,
                Rating: rating,
                PlaceId:id

            },

            success: (function (data) {
                alert("ok");
            })


        });


    });
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
