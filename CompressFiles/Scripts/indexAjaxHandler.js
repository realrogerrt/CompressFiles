/// <reference path="jquery-1.10.2.js" />


//Ajax upload handler:

$(function () {
    var files;
    $("#submit-file").on("change", function (ev) {
        files = ev.target.files;
    });

    $("form").on("submit", asyncUpload);

    function asyncUpload(ev) {
        ev.defaultPrevented();
        var data = new FormData();
        $.each(files, function (k, v) {
            data.append(k, v);
        });

        $.ajax({
            url: "/File/FileHandler",
            type: "POST",
            data: data,
            success: function (e) {
                alert("success");
            },
            error: function (e) {
                alert("fail");
            }
        });
    };

    alert("reached the end");

});