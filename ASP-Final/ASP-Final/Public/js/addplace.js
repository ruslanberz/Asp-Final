$(document).ready(function () {
    var ser = [];
    
    $(document).on("click", "#services input", function () {


        if ($(this).prop("checked") === true) {
            ser.push($(this).data("id"));
            

        } else {
            ser.splice($.inArray($(this).data("id"), ser), 1);
            
        }
    });

    $("#AddForm").submit(function (ev) {
        ev.preventDefault();
        //var service = JSON.stringify(servis);
        var formdata = new FormData(); //FormData object
        var files = $("#files").get(0).files;
        //Iterating through each files selected in fileInput
        for (var i = 0; i < files.length; i++) {
            //Appending each file to FormData object
            
            formdata.append(files[i].name, files[i]);
        }
        //console.log(servis);
        //formdata.set('servis', servis);
        var times = [];

        for (var i = 1; i < 8; i++) {
            if ($('#w' + i).is(":checked")) {

                if ($('.w' + i + '1 option:selected').text() == "Opening Time" || $('.w' + i + '2 option:selected').text() == "Closing Time") {


                } else {

                    var open = $('.w' + i + '1 option:selected').text();
                    var close = $('.w' + i + '2 option:selected').text();
                    open = open.substring(0, open.length - 2);
                    buf = open.substring(0, open.indexOf(':')-1);
                    open = buf + open.substring(open.indexOf(':'), open.length+1)
                    close = close.substring(0, close.length - 2);
                    buf = close.substring(0, close.indexOf(':') - 1);
                    close = buf + close.substring(close.indexOf(':'), close.length + 1)
                    times.push({

                        OpenHour:open ,
                        CloseHour: close,
                        WeekNo: i

                    });
                    console.log(times);
                }


            };
        };
       
       
        var forCreateTwo;
        $.ajax({
            url: $(this).attr("action"),
            type: $(this).attr("method"),
            data: new FormData(this),
            dataType: "json",
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == 401) {

                    toastr["error"](data.message);
                }

                else {

                    forCreateTwo = data.placeId;



                    var postData = {
                        ser: ser,
                        times: times,
                        placeId: forCreateTwo
                    }



                    $.ajax({
                        type: "post",
                        url: "/place/create2",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(postData),
                        success: function (data) {

                            toastr["success"](data.message);
                            setInterval(function () { window.location = data.url; }, 2000);


                        }
                    });

                }
               
            },
            error: function (xhr, error, status) {
                console.log(error, status);
            }
        });
       

    });
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


   
});

