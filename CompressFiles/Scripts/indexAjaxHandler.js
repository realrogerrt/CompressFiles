/// <reference path="jquery-1.10.2.js" />


//Ajax upload handler:

$(function () {

    $("#submit-file").click(function (ev) {
        ev.preventDefault();
        var data;
        $("originalFile").data(data);
        console.log(data);
        var grandPa = $(this).parent().parent();
        grandPa.prev().html("Uploading..");
        grandPa.children().remove();
    });

    console.log("reached the end");
});