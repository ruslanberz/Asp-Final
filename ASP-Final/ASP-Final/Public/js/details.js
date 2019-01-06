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
            $("#rateval").val(currentRating);
            console.log($("#rateval").val());
        }
    });
    $("#rate").submit(function (e) {
        e.preventDefault();
        var formdata = new FormData(); //FormData object
        var files = $("#filess").get(0).files;
        //Iterating through each files selected in fileInput
        for (var i = 0; i < files.length; i++) {
            //Appending each file to FormData object
            console.log(files[i].name);
            formdata.append(files[i].name, files[i]);
        }
        console.log($(this).serialize());
        $.ajax({
            type: 'post',
            dataType: "json",
            url: "/Place/PublishComment",
            cache: false,
            contentType: false,
            processData: false,
            data: new FormData(this) ,

            success: (function (data) {
                console.log(data);
                window.location.href = data.url;
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
    $(document).on("click", "#helpful", function () {
    
        var CommentId = Number($(this).data("id"));
        console.log(CommentId);
        
        var IsAdd;
      
        if (!$(this).hasClass("hgreen")) {
            $(this).siblings().each(function () {
                if ($(this).hasClass("unhelpful")) {
                    $(this).removeClass("uhred");
                }
            });
        
            IsAdd = true;
            $(this).addClass("hgreen");
            $.ajax({
                url: "/Place/Hcomment",
                type: 'post',
                dataType: 'json',
                data:  { CommentId, IsAdd },

                success: function (data) {
                    console.log(data.message);
                }



            })
        }

        else {
            $(this).removeClass("hgreen");
            $(this).siblings().each(function () {
                if ($(this).hasClass("unhelpful")) {
                    $(this).removeClass("uhred");
                }
            });
            $(".unhelpful").each(function (index, item) {
                if ($(".unhelpful").data("id") == $(this).data("id")) {
                    console.log(index);
                    $('.unhelpful:eq(' + index + ')').removeClass("uhred");
                }
            });
            console.log("here");
            IsAdd = false;
            $.ajax({
                url: "/place/hcomment",
                type: 'post',
                dataType: 'json',
                data: { CommentId, IsAdd },

                success: function (data) {
                    console.log(data.message);
                    console.log("#sildimblet");
                }



            })


        }
    });
    $(document).on("click", "#unhelpful", function () {

        var CommentId = Number($(this).data("id"));
        console.log(CommentId);

        var IsAdd;

        if (!$(this).hasClass("uhred")) {
            $(this).siblings().each(function () {
                if ($(this).hasClass("helpful")) {
                    $(this).removeClass("hgreen");
                }
            });
            $(".helpful").each(function (index, item) {
                if ($(".helpful").data("id") == $(this).data("id")) {
                    console.log(index);
                    $(this).removeClass("hgreen");
                }
            });
            IsAdd = true;
            $(this).addClass("uhred");
            $.ajax({
                url: "/Place/Uhcomment",
                type: 'post',
                dataType: 'json',
                data: { CommentId, IsAdd },

                success: function (data) {
                    console.log(data.message);
                }



            })
        }

        else {
            $(this).siblings().each(function () {
                if ($(this).hasClass("helpful")) {
                    $(this).removeClass("hgreen");
                }
            });
            $(this).removeClass("uhred");
            $(".helpful").each(function (index, item) {
                if ($(".helpful").data("id") == $(this).data("id")) {
                    console.log(index);
                    $(this).removeClass("hgreen");
                }
            });
            console.log("here");
            IsAdd = false;
            $.ajax({
                url: "/place/Uhcomment",
                type: 'post',
                dataType: 'json',
                data: { CommentId, IsAdd },

                success: function (data) {
                    console.log(data.message);
                    console.log("#sildimblet");
                }



            })


        }
    });
});
