﻿/// <reference path="jquery-1.10.2.js" />

/// <reference path="jquery-1.10.2.js" />


//Ajax upload handler uncompress page:

//I need to organize this javascript like a reusable jquery plugin

$(function () {
    function beforeSendHandler() {
        console.log("Before");
    }

    function completeHandler(res) {
        console.log("Success");
        console.log(res);
        var formPrev = $("form.main-form").prev();
        formPrev.text("Upload complete!");
        $("#upload-progress-bar").addClass("progress-striped");
        $("#upload-progress-bar").addClass("active");
        //setTimeout(function () {
        formPrev.fadeOut();
        formPrev.text("Uncompressing..");
        formPrev.fadeIn();
        //}, 1000);
        //Convertion way ajax
        function handleConvertionSuccess() {
            formPrev.text("Successful operation!");
            var anchor = '<a href="/File/GetOriginal">Download the original file here. </a>';
            $(".progress").remove();
            formPrev.parent().append(anchor);
        }

        function handleConvertionFail() {
            alert("Something wrong happened :(");
        }
        $.ajax({
            type: "Post",
            url: "/File/UnCompressAsync",
            success: handleConvertionSuccess,
            error: handleConvertionFail,
        });
    }

    function errorHandler(ev, type, msg) {
        console.log("Error");
        console.log(ev);
        console.log(type);
        console.log(msg);
    }

    function progressHandlingFunction(ev) {
        console.log("progress:");
        console.log(ev);
        var loaded = ev.loaded;
        var total = ev.total;
        var percent = (loaded * 100) / total;
        var bar = $("#upload-progress-bar .progress-bar");
        bar.css({ width: percent + "%" });
        percent = Math.floor(percent)
        bar.text(percent + "%");
    }

    $("#submit-compressed-file").click(function (ev) {
        ev.preventDefault();
        var grandPa = $(this).parent().parent();//Form tag
        var formData = new FormData($(this).parents("form")[0]);
        grandPa.prev().html("Uploading..");
        grandPa.children().remove();
        var progressBar = '<div id="upload-progress-bar" class="progress">\
    <div class="progress-bar" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" style="width: 0%">\
        <span class="sr-only"></span>\
    </div>\
</div>';
        grandPa.append(progressBar);
        $.ajax({
            url: "/File/PostCompressedFile",
            type: "POST",
            xhr: function () {  // Custom XMLHttpRequest
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) { // Check if upload property exists
                    myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
                }
                return myXhr;
            },
            beforeSend: beforeSendHandler,
            success: completeHandler,
            error: errorHandler,
            // Form data
            data: formData,
            //Options to tell jQuery not to process data or worry about content-type.
            cache: false,
            contentType: false,
            processData: false
        });
    });

    console.log("reached the end");
});