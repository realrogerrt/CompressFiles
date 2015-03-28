/// <reference path="jquery-1.10.2.js" />


//Ajax upload handler:

$(function () {
    function beforeSendHandler() {
        console.log("Before");
    }
    function completeHandler(res) {
        console.log("Success");
        console.log(res);
        var formPrev = $("form").prev();
        formPrev.text("Upload complete!");
        $("#upload-progress-bar").css({ width: "0%" });
        setTimeout(function () {
            formPrev.fadeOut();
            formPrev.text("Compressing..");
            formPrev.fadeIn();
        }, 1000);
        //Convertion way ajax
        function handleConvertionSuccess() {
            alert("!!!!!!!!!!!!!!");
            formPrev.text("Successful operation!");
            var anchor = '<a href="/File/GetConvertedFile">Download the converted file here. </a>';
            formPrev.parent().append(anchor);
        }

        function handleConvertionFail() {
            alert("Something wrong happened :(");
        }
        $.ajax({
            type: "Post",
            url: "/File/Converter",
            success: handleConvertionSuccess,
            error: handleConvertionFail,
        });
    }
    function errorHandler(ev,type, msg) {
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
        var bar = $("#upload-progress-bar");
        bar.css({ width: percent + "%" });
        percent = Math.floor(percent)
        bar.text(percent + "%");
    }
    $("#submit-file").click(function (ev) {
        ev.preventDefault();
        var grandPa = $(this).parent().parent();//Form tag
        var formData = new FormData($("form")[0]);
        grandPa.prev().html("Uploading..");
        grandPa.children().remove();
        var progressBar = '<div class="progress">\
  <div id="upload-progress-bar" class="progress-bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width:0%;">\
  0%\
  </div>\
</div>';
        grandPa.append(progressBar);
        $.ajax({
            url: "/File/FileHandler",
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