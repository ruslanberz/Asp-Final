$(document).ready(function () {


   //Map point add finish


    // Weekdays Toggles
    $("#w7").change(function (event) {
        if (this.checked) {
            $(".w71").removeClass("d-none");
            $(".w72").removeClass("d-none");
        } else {
            $(".w71").addClass("d-none");
            $(".w72").addClass("d-none");
        }
    });
    $("#w6").change(function (event) {
        if (this.checked) {
            $(".w61").removeClass("d-none");
            $(".w62").removeClass("d-none");
        } else {
            $(".w61").addClass("d-none");
            $(".w62").addClass("d-none");
        }
    });
    $("#w5").change(function (event) {
        if (this.checked) {
            $(".w51").removeClass("d-none");
            $(".w52").removeClass("d-none");
        } else {
            $(".w51").addClass("d-none");
            $(".w52").addClass("d-none");
        }
    });
    $("#w4").change(function (event) {
        if (this.checked) {
            $(".w41").removeClass("d-none");
            $(".w42").removeClass("d-none");
        } else {
            $(".w41").addClass("d-none");
            $(".w42").addClass("d-none");
        }
    });
    $("#w3").change(function (event) {
        if (this.checked) {
            $(".w31").removeClass("d-none");
            $(".w32").removeClass("d-none");
        } else {
            $(".w31").addClass("d-none");
            $(".w32").addClass("d-none");
        }
    });
    $("#w2").change(function (event) {
        if (this.checked) {
            $(".w21").removeClass("d-none");
            $(".w22").removeClass("d-none");
        } else {
            $(".w21").addClass("d-none");
            $(".w22").addClass("d-none");
        }
    });
    $("#w1").change(function (event) {
        if (this.checked) {
            $(".w11").removeClass("d-none");
            $(".w12").removeClass("d-none");
        } else {
            $(".w11").addClass("d-none");
            $(".w12").addClass("d-none");
        }
    });
    //Weekdays toggle end

    //Form Submit

 

    //Form Submit end

    $("#PlaceBlyat").click( function (e) {
        e.preventDefault();
        console.log("dcd");
        $.ajax({
            url: "/home/checklogin",
            type: "post",
            dataType: "json",
            success: function (response)
            {
                if (response.status === 200) {

                    window.location = response.url;


                }

                else {

                    $("#LoginModal").modal("show");
                }
            }
        });

       

    });
  


    $("#LoginForm").submit(function (ev) {

        ev.preventDefault();
        $("#LoginError").text("");
        $.ajax({
            url: "/home/login",
            type: "post",
            dataType: "json",
            data: $(this).serialize(),
            success: function (response) {
                
                if (response.status === 401) {
                    
                    $("#LoginError").text(response.message);  
                }

                if (response.status === 200)
                {
                    //$("#LoginModal").modal("hide");
                   
                    window.location = response.url;
                }

            },
            //error: function (response) {



            //}





        });

    });
  
 
});