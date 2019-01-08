$(document).ready(function () {

    $("#login").click(function (ex) {
        ex.preventDefault();
   

        $.ajax({
            url: "/home/dashboard",
            type: "post",
            dataType: "json",
            data: $(this).serialize(),
            success: function (data) {

                if (data.status==200) {

                    window.location.href = data.url;
                }

            }


        })


    });

});